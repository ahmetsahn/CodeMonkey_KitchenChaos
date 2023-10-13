using System;
using System.Collections;
using _Scripts.Data.KitchenObjectsData;
using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.Units.Counter.CuttingCounter
{
    public class CuttingCounterFacade : BaseCounterFacade
    {
        private CuttingCounterView _cuttingCounterView;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectsData _kitchenObjectsData;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        private CuttingCounterAnimationHandler _cuttingCounterAnimationHandler;
        
        [Inject]
        private void Construct(
            CuttingCounterView cuttingCounterView,
            PlayerSignals playerSignals,
            KitchenObjectsData kitchenObjectsData,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal,
            CuttingCounterAnimationHandler cuttingCounterAnimationHandler)
        {
            _cuttingCounterView = cuttingCounterView;
            _playerSignals = playerSignals;
            _kitchenObjectsData = kitchenObjectsData;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
            _cuttingCounterAnimationHandler = cuttingCounterAnimationHandler;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged += OnKitchenObjectOwnedByThePlayerChanged;
        }
        
        private void OnKitchenObjectOwnedByThePlayerChanged(KitchenObjectOwnedByThePlayerParams kitchenObjectOwnedByThePlayerParams)
        {
            _cuttingCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }

        public override void Interact()
        {
            if (IsCanTake())
            {
                _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.
                    Invoke(_cuttingCounterView.KitchenObjectSpawnPositionOnCounter);
                
                _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
                (new KitchenObjectOwnedByThePlayerParams()
                {
                    KitchenObjectOwnedByThePlayer = _cuttingCounterView.KitchenObjectOnTheCounter
                });
                
                _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.
                    Invoke(_cuttingCounterView.KitchenObjectOnTheCounter,
                        _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
                
                _cuttingCounterView.KitchenObjectOnTheCounter = KitchenObjects.Empty;
                
                Deselect();
                
                return;
            }
            
            if (IsCanPut())
            {
                _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.
                    Invoke(_playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
                
                _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.
                    Invoke(_cuttingCounterView.KitchenObjectOwnedByThePlayer,_cuttingCounterView.KitchenObjectSpawnPositionOnCounter);
                
                _cuttingCounterAnimationHandler.PlayCutAnimation();
                _playerSignals.OnDisableInteractRaycast?.Invoke();
                Deselect();
                
                StartCoroutine(AnimationTimer());
                
                
            }
        }

        public override bool Select()
        {
            if(!IsCanTake() && !IsCanPut()) return false;
            _cuttingCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _cuttingCounterView.SelectedCounter.SetActive(false);
        }

        private bool IsCanTake()
        {
            return _cuttingCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty &&
                   _cuttingCounterView.KitchenObjectOnTheCounter != KitchenObjects.Empty;
        }
        
        private bool IsCanPut()
        {
            return _kitchenObjectsData.SliceableKitchenObjectsList.Contains(_cuttingCounterView.KitchenObjectOwnedByThePlayer) &&
                   _cuttingCounterView.KitchenObjectOnTheCounter == KitchenObjects.Empty;
        }
        
        private void UnsubscribeEvents()
        {
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged -= OnKitchenObjectOwnedByThePlayerChanged;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        private IEnumerator AnimationTimer()
        {
            yield return _cuttingCounterAnimationHandler._cutAnimationWaitForSeconds;
            
            _cuttingCounterAnimationHandler.StopCutAnimation();
            
            _cuttingCounterView.KitchenObjectOnTheCounter =
                _kitchenObjectsData.SlicedKitchenObjectsDictionary
                    [_cuttingCounterView.KitchenObjectOwnedByThePlayer];
                
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
            (new KitchenObjectOwnedByThePlayerParams()
            {
                KitchenObjectOwnedByThePlayer = KitchenObjects.Empty
            });
            
            _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.
                Invoke(_cuttingCounterView.KitchenObjectSpawnPositionOnCounter);
                
            _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.
                Invoke(_cuttingCounterView.KitchenObjectOnTheCounter,_cuttingCounterView.KitchenObjectSpawnPositionOnCounter);
                
            _playerSignals.OnEnableInteractRaycast?.Invoke();
        }
    }
}
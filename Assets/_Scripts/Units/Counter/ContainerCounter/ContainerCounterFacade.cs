using System;
using System.Collections;
using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.Units.Counter.ContainerCounter
{
    public class ContainerCounterFacade : BaseCounterFacade
    {
        private ContainerCounterView _containerCounterView;
        private ContainerCounterAnimationHandler _containerCounterAnimationHandler;
        private PlayerSignals _playerSignals;
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        [Inject]
        public void Construct(
            ContainerCounterView containerCounterView,
            ContainerCounterAnimationHandler containerCounterAnimationHandler,
            PlayerSignals playerSignals,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal)
        {
            _containerCounterView = containerCounterView;
            _containerCounterAnimationHandler = containerCounterAnimationHandler;
            _playerSignals = playerSignals;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
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
            _containerCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }
        
        public override void Interact()
        {
            if(!IsCanTake()) return;
         
            _containerCounterAnimationHandler.PlayOpenAnimation();
            
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
            (new KitchenObjectOwnedByThePlayerParams()
            {
                KitchenObjectOwnedByThePlayer = _containerCounterView.KitchenObjectOnTheCounter
            });
            
            _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.
                Invoke(_containerCounterView.KitchenObjectOnTheCounter,_playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
            
            Deselect();
        }

        public override bool Select()
        {
            if(!IsCanTake()) return false;
            _containerCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _containerCounterView.SelectedCounter.SetActive(false);
        }

        private bool IsCanTake()
        {
            return _containerCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty;
        }
        
        private void UnsubscribeEvents()
        {
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged -= OnKitchenObjectOwnedByThePlayerChanged;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
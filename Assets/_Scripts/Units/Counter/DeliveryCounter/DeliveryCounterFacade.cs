using System;
using System.Linq;
using _Scripts.Data.CoreGameData;
using _Scripts.Data.KitchenObjectsData;
using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using Zenject;

namespace _Scripts.Units.Counter.DeliveryCounter
{
    public class DeliveryCounterFacade : BaseCounterFacade
    {
        private DeliveryCounterView _deliveryCounterView;
        
        private DeliveryCounterGUI _deliveryCounterGUI;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        private CoreGameSignals _coreGameSignals;
        
        private ListSignals _listSignals;
        
        
        [Inject]
        private void Construct(
            DeliveryCounterView deliveryCounterView,
            DeliveryCounterGUI deliveryCounterGUI,
            PlayerSignals playerSignals,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal,
            CoreGameSignals coreGameSignals,
            ListSignals listSignals)
        {
            _deliveryCounterView = deliveryCounterView;
            _deliveryCounterGUI = deliveryCounterGUI;
            _playerSignals = playerSignals;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
            _coreGameSignals = coreGameSignals;
            _listSignals = listSignals;
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
            _deliveryCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }

        public override void Interact()
        {
            if (!IsCanPut()) return;

            if (IsTrueOrder())
            {
                _deliveryCounterGUI.HandleDeliveryCounterSuccessGUI();
                _coreGameSignals.OnSuccessOrder?.Invoke();
            }

            else
            {
                _deliveryCounterGUI.HandleDeliveryCounterFailGUI();
                _coreGameSignals.OnFailOrder?.Invoke();
            }
            
            _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.
                Invoke(_playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
            
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
            (new KitchenObjectOwnedByThePlayerParams()
            {
                KitchenObjectOwnedByThePlayer = KitchenObjects.Empty
            });
                
            Deselect();
            
            
        }

        public override bool Select()
        {
            if (!IsCanPut()) return false;
            _deliveryCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _deliveryCounterView.SelectedCounter.SetActive(false);
        }
        
        private bool IsCanPut()
        {
            return _deliveryCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Plate;
        }

        private bool IsTrueOrder()
        {
            return _listSignals.OnIsOrderListAndPlaneListEqual.Invoke();
        }
    }
}
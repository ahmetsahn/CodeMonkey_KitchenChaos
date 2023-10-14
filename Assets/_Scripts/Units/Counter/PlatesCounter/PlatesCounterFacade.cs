using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using Zenject;

namespace _Scripts.Units.Counter.PlatesCounter
{
    public class PlatesCounterFacade : BaseCounterFacade
    {
        private PlatesCounterView _platesCounterView;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        
        [Inject]
        public void Construct(
            PlatesCounterView platesCounterView,
            PlayerSignals playerSignals,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal)
        {
            _platesCounterView = platesCounterView;
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
            _platesCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }

        public override void Interact()
        {
            if (_platesCounterView.KitchenObjectOwnedByThePlayer != KitchenObjects.Empty) return;
            
            _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.
                Invoke(_platesCounterView.KitchenObjectOnTheCounter,
                    _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
            
            _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
            (new KitchenObjectOwnedByThePlayerParams()
            {
                KitchenObjectOwnedByThePlayer = _platesCounterView.KitchenObjectOnTheCounter
            });
            
            Deselect();
        }

        public override bool Select()
        {
            if(_platesCounterView.KitchenObjectOwnedByThePlayer != KitchenObjects.Empty) return false;
            _platesCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _platesCounterView.SelectedCounter.SetActive(false);
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
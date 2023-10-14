using _Scripts.Signals;
using _Scripts.Enums;
using _Scripts.Keys;
using Zenject;

namespace _Scripts.Units.Counter.TrashCounter
{
    public class TrashCounterFacade : BaseCounterFacade
    {
        private TrashCounterView _trashCounterView;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        [Inject]
        public void Construct(
            TrashCounterView trashCounterView, 
            PlayerSignals playerSignals,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal)
        {
            _trashCounterView = trashCounterView;
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
            _trashCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }
        
        public override void Interact()
        {
            if(_trashCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty) return;
            
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
            if(_trashCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty) return false;
            _trashCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _trashCounterView.SelectedCounter.SetActive(false);
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
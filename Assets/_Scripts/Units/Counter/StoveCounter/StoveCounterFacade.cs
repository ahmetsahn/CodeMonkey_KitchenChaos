using _Scripts.Data.KitchenObjectsData;
using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using Zenject;

namespace _Scripts.Units.Counter.StoveCounter
{
    public class StoveCounterFacade : BaseCounterFacade
    {
        private StoveCounterView _stoveCounterView;
        
        private StoveCounterGUI _stoveCounterGUI;
        
        private StoveCounterVisual _stoveCounterVisual;
        
        private KitchenObjectsData _kitchenObjectsData;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        
        [Inject]
        public void Construct(
            StoveCounterView stoveCounterView,
            StoveCounterGUI stoveCounterGUI,
            StoveCounterVisual stoveCounterVisual,
            PlayerSignals playerSignals,
            KitchenObjectsData kitchenObjectsData,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal)
        {
            _stoveCounterView = stoveCounterView;
            _stoveCounterGUI = stoveCounterGUI;
            _stoveCounterVisual = stoveCounterVisual;
            _playerSignals = playerSignals;
            _kitchenObjectsData = kitchenObjectsData;
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
            _stoveCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }

        public override void Interact()
        {
            
            if (_stoveCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty &&
                _stoveCounterView.KitchenObjectOnTheStove != KitchenObjects.Empty)
            {
                _stoveCounterGUI.TakeKitchenObjectFromTheStove();
                _stoveCounterVisual.TakeKitchenObjectFromTheStove();
                
                _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects?.Invoke(
                    _stoveCounterView.KitchenObjectSpawnPositionOnCounter,
                    _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
                
                _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
                (new KitchenObjectOwnedByThePlayerParams()
                {
                    KitchenObjectOwnedByThePlayer = _stoveCounterView.KitchenObjectOnTheStove
                });
                
                if(!_kitchenObjectsData.CookableKitchenObjectsList.Contains(_stoveCounterView.KitchenObjectOnTheStove)) 
                    Deselect();
                
                _stoveCounterView.KitchenObjectOnTheStove = KitchenObjects.Empty;
                
                return;
            }
            
            if (_kitchenObjectsData.CookableKitchenObjectsList.Contains(_stoveCounterView.KitchenObjectOwnedByThePlayer) &&
                _stoveCounterView.KitchenObjectOnTheStove == KitchenObjects.Empty)
            {
                _stoveCounterGUI.PutKitchenObjectOnTheStove();
                _stoveCounterVisual.PutKitchenObjectOnTheStove();
                
                _stoveCounterView.KitchenObjectOnTheStove = _stoveCounterView.KitchenObjectOwnedByThePlayer;
                
                _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects?.Invoke(
                    _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke(),
                    _stoveCounterView.KitchenObjectSpawnPositionOnCounter);
                
                _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
                (new KitchenObjectOwnedByThePlayerParams()
                {
                    KitchenObjectOwnedByThePlayer = KitchenObjects.Empty
                });
                
            }
        }

        public override bool Select()
        {
            if ((!_kitchenObjectsData.CookableKitchenObjectsList.Contains(_stoveCounterView
                     .KitchenObjectOwnedByThePlayer) ||
                 !_kitchenObjectsData.CookableKitchenObjectsList.Contains(_stoveCounterView.KitchenObjectOwnedByThePlayer) ||
                 _stoveCounterView.KitchenObjectOnTheStove != KitchenObjects.Empty) &&
                (_stoveCounterView.KitchenObjectOwnedByThePlayer != KitchenObjects.Empty ||
                 _stoveCounterView.KitchenObjectOnTheStove == KitchenObjects.Empty)) return false;
            _stoveCounterView.SelectedCounter.SetActive(true);
            return true;
        }

        public override void Deselect()
        {
            _stoveCounterView.SelectedCounter.SetActive(false);
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
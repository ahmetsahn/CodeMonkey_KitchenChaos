using _Scripts.Data.KitchenObjectsData;
using _Scripts.Enums;
using _Scripts.Keys;
using _Scripts.Signals;
using Zenject;

namespace _Scripts.Units.Counter.ClearCounter
{
    public class ClearCounterFacade : BaseCounterFacade
    {
        private ClearCounterView _clearCounterView;
        
        private PlayerSignals _playerSignals;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        private KitchenObjectsData _kitchenObjectsData;
        
        private ListSignals _listSignals;
        
        private PlateSignals _plateSignals;
        
        private bool _isPutFoodOnThePlate;

        private bool _isPutThePlateOnTheCounter;
        
        
        [Inject]
        public void Construct(
            ClearCounterView clearCounterView, 
            PlayerSignals playerSignals,
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal,
            KitchenObjectsData kitchenObjectsData,
            ListSignals listSignals,
            PlateSignals plateSignals)
        {
            _clearCounterView = clearCounterView;
            _playerSignals = playerSignals;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
            _kitchenObjectsData = kitchenObjectsData;
            _listSignals = listSignals;
            _plateSignals = plateSignals;
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
            _clearCounterView.KitchenObjectOwnedByThePlayer = kitchenObjectOwnedByThePlayerParams.KitchenObjectOwnedByThePlayer;
        }

        public override void Interact()
        {
            if (IsCanTake())
            {
                //TODO: Add logic for taking the object from the counter
                
                _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
                (new KitchenObjectOwnedByThePlayerParams()
                {
                    KitchenObjectOwnedByThePlayer = _clearCounterView.KitchenObjectOnTheCounter
                });
                
                _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects?.Invoke(
                    _clearCounterView.KitchenObjectSpawnPositionOnCounter,
                    _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke());
                
                _clearCounterView.KitchenObjectOnTheCounter = KitchenObjects.Empty;
                
                return;
            }

            if (IsCanPut())
            {
                //TODO: Add logic for putting the object on the counter
                
                if(_isPutFoodOnThePlate)
                {
                    _listSignals.OnAddToPlaneList.Invoke(_clearCounterView.KitchenObjectOwnedByThePlayer);
                    _plateSignals.OnSetActivePlateIcon?.Invoke(_clearCounterView.KitchenObjectOwnedByThePlayer);
                    _isPutFoodOnThePlate = false;
                }
                
                if(_isPutThePlateOnTheCounter)
                {
                    _listSignals.OnAddToPlaneList.Invoke(_clearCounterView.KitchenObjectOnTheCounter);
                    _plateSignals.OnSetActivePlateIcon?.Invoke(_clearCounterView.KitchenObjectOnTheCounter);
                    _isPutThePlateOnTheCounter = false;
                }
                
                if(_clearCounterView.KitchenObjectOnTheCounter != KitchenObjects.Plate)
                    _clearCounterView.KitchenObjectOnTheCounter = _clearCounterView.KitchenObjectOwnedByThePlayer;
                
                _playerSignals.OnKitchenObjectOwnedByThePlayerChanged?.Invoke
                (new KitchenObjectOwnedByThePlayerParams()
                {
                    KitchenObjectOwnedByThePlayer = KitchenObjects.Empty
                });
                
                _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects?.Invoke(_playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer?.Invoke(),
                    _clearCounterView.KitchenObjectSpawnPositionOnCounter);
            }
        }

        public override bool Select()
        {
            if (!IsCanTake() && !IsCanPut()) return false;
            _clearCounterView.SelectedCounter.SetActive(true);
            return true;
        }
        
        private bool IsCanTake()
        {
            return _clearCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Empty &&
                   _clearCounterView.KitchenObjectOnTheCounter != KitchenObjects.Empty;
        }
        
        private bool IsCanPut()
        {
            if(_clearCounterView.KitchenObjectOwnedByThePlayer != KitchenObjects.Empty &&
                    _clearCounterView.KitchenObjectOnTheCounter == KitchenObjects.Empty)
            {
                return true;
            }

            if ((_kitchenObjectsData.PlateableKitchenObjectsList.Contains(_clearCounterView
                     .KitchenObjectOwnedByThePlayer) &&
                 _clearCounterView.KitchenObjectOnTheCounter == KitchenObjects.Plate))
            {
                _isPutFoodOnThePlate = true;
                return true;
            }
            
            if(_clearCounterView.KitchenObjectOwnedByThePlayer == KitchenObjects.Plate &&
                _kitchenObjectsData.PlateableKitchenObjectsList.Contains(_clearCounterView
                    .KitchenObjectOnTheCounter))
            {
                _isPutThePlateOnTheCounter = true;
                return true;
            }
            
            return false;   
        }

        public override void Deselect()
        {
            _clearCounterView.SelectedCounter.SetActive(false);
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
using System;
using _Scripts.Data.PlayerData;
using _Scripts.Keys;
using _Scripts.Signals;
using _Scripts.Units.Counter;
using UnityEngine;
using Zenject;

namespace _Scripts.Units.Player
{
    public class PlayerInteractHandler : ITickable, IDisposable
    {
        private readonly PlayerView _playerView;
        
        private readonly PlayerInteractData _playerInteractData;
        
        private readonly PlayerSignals _playerSignals;

        private readonly InputSignals _inputSignals;
        
        private BaseCounterFacade _selectedBaseCounter;
        
        private Vector3 _moveDirection;
        
        private bool _isRaycastEnabled = true;
        private bool _isSelectable;
        
        public PlayerInteractHandler(
            PlayerView playerView,
            PlayerInteractData playerInteractData,
            PlayerSignals playerSignals,
            InputSignals inputSignals)
        {
            _playerView = playerView;
            _playerInteractData = playerInteractData;
            _playerSignals = playerSignals;
            _inputSignals = inputSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputSignals.OnInputTaken += OnInputTaken;
            _inputSignals.OnInteractTaken += OnInteractTaken;
            _playerSignals.OnEnableInteractRaycast += OnEnableInteractRaycast;
            _playerSignals.OnDisableInteractRaycast += OnDisableInteractRaycast;
        }
        
        private void OnInputTaken(InputParams inputParams)
        {
            _moveDirection = inputParams.MoveDirection;
        }
        
        private void OnInteractTaken()
        {
            if(!_isSelectable) return;
            _selectedBaseCounter.Interact();
        }
        
        private void OnEnableInteractRaycast()
        {
            _isRaycastEnabled = true;
        }
        
        private void OnDisableInteractRaycast()
        {
            _isRaycastEnabled = false;
            _selectedBaseCounter = null;
        }
      
        public void Interact()
        {
            if(Physics.Raycast(
                   _playerView.RaycastOriginPosition, 
                   _moveDirection, 
                   out var raycastHit, 
                   _playerInteractData.InteractDistance,
                   _playerView.CountersLayerMask))
            {
                if (!raycastHit.collider.TryGetComponent(out BaseCounterFacade counterFacade)) return;
                
                if(_selectedBaseCounter == counterFacade) return;
                if(_selectedBaseCounter != null) _selectedBaseCounter.Deselect();
                _selectedBaseCounter = counterFacade;
                _isSelectable = counterFacade.Select();
            }

            else
            {
                if(_selectedBaseCounter == null) return;
                _selectedBaseCounter.Deselect();
                _selectedBaseCounter = null;
                _isSelectable = false;
            }
        }

        public void Tick()
        {
            if(!_isRaycastEnabled) return;
            Interact();
        }
        
        private void UnsubscribeEvents()
        {
            _inputSignals.OnInputTaken -= OnInputTaken;
            _inputSignals.OnInteractTaken -= OnInteractTaken;
            _playerSignals.OnEnableInteractRaycast -= OnEnableInteractRaycast;
            _playerSignals.OnDisableInteractRaycast -= OnDisableInteractRaycast;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
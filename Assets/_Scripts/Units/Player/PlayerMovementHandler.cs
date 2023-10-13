using System;
using _Scripts.Data.PlayerData;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;

namespace _Scripts.Units.Player
{
    public class PlayerMovementHandler : IDisposable
    {
        private readonly PlayerView _playerView;
        
        private readonly PlayerMovementData _playerMovementData;
        
        private readonly InputSignals _inputSignals;
        
        private Vector3 _moveDirection;
        
        public PlayerMovementHandler(
            PlayerView playerView,
            InputSignals inputSignals,
            PlayerMovementData playerMovementData)
        {
            _playerView = playerView;
            _inputSignals = inputSignals;
            _playerMovementData = playerMovementData;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputSignals.OnInputTaken += OnInputTaken;
        }
        
        private void OnInputTaken(InputParams inputParams)
        {
            _moveDirection = inputParams.MoveDirection;
            _playerView.PlayerRigidbody.velocity = _moveDirection * _playerMovementData.MovementSpeed;
        }
        
        private void UnsubscribeEvents()
        {
            _inputSignals.OnInputTaken -= OnInputTaken;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}

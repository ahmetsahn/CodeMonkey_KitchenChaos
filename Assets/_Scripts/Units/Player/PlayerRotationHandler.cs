using System;
using _Scripts.Data.PlayerData;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;

namespace _Scripts.Units.Player
{
    public class PlayerRotationHandler : IDisposable
    {
        private readonly PlayerView _playerView;
        
        private readonly PlayerRotationData _playerRotationData;
        
        private readonly InputSignals _inputSignals;
        
        private Vector3 _moveDirection;
        
        
        public PlayerRotationHandler(
            PlayerView playerView,
            PlayerRotationData playerRotationData,
            InputSignals inputSignals)
        {
            _playerView = playerView;
            _playerRotationData = playerRotationData;
            _inputSignals = inputSignals;
            
            SubscribeEvents();
        }
        
        public void SubscribeEvents()
        {
            _inputSignals.OnInputTaken += OnInputTaken;
        }
        
        private void OnInputTaken(InputParams inputParams)
        {
            _moveDirection = inputParams.MoveDirection;
            RotatePlayer(_moveDirection.x, _moveDirection.z);
        }
        
        public void RotatePlayer(float horizontal, float vertical)
        {
            var movement = new Vector3(horizontal, 0f, vertical);
            var targetRotation = Quaternion.LookRotation(movement.normalized);
            _playerView.PlayerTransform.rotation = Quaternion.Lerp(
                _playerView.PlayerTransform.rotation, targetRotation, _playerRotationData.RotationSpeed * Time.deltaTime);
        }
        
        public void UnsubscribeEvents()
        {
            _inputSignals.OnInputTaken -= OnInputTaken;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
using System;
using _Scripts.Data.InputData;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class InputManager : ITickable, IDisposable
    {
        private readonly CoreGameSignals _coreGameSignals;
        
        private readonly InputData _inputData;
        
        private float _horizontal;
        private float _vertical;
        
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        private Vector3 _moveDirection;
        
        private InputParams _inputParams;
        
        private readonly InputSignals _inputSignals;
        
        private bool _isEnableInput;
        

        public InputManager(
            CoreGameSignals coreGameSignals,
            InputSignals inputSignals,
            InputData inputData)
        {
            _coreGameSignals = coreGameSignals;
            _inputSignals = inputSignals;
            _inputData = inputData;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _coreGameSignals.OnPlayStarted += EnableInput;
        }
        
        public void Tick()
        {
            if(!_isEnableInput) return;
            
            _horizontal = Input.GetAxis(HorizontalAxis);
            _vertical = Input.GetAxis(VerticalAxis);
            _moveDirection.x = _horizontal;
            _moveDirection.z = _vertical;
            
            if (Input.GetKeyDown(_inputData.InteractKey))
            {
                
                _inputSignals.OnInteractTaken?.Invoke();
            }
            
            if(_moveDirection == Vector3.zero)
            {
                _inputSignals.OnInputReleased?.Invoke();
                return;
            }
            
            _inputSignals.OnInputTaken?.Invoke(new InputParams()
            {
                MoveDirection = _moveDirection
            });
        }
        
        private void EnableInput()
        {
            _isEnableInput = true;
        }
        
        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnPlayStarted -= EnableInput;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
using System;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;

namespace _Scripts.Units.Player
{
    public class PlayerAnimationHandler : IDisposable
    {
        private readonly PlayerView _playerView;
        
        private readonly InputSignals _inputSignals;
       
        private int _currentState;
        private static readonly int Idle = Animator.StringToHash("PlayerIdle");
        private static readonly int Walk = Animator.StringToHash("PlayerWalk");
        
        private Vector3 _moveDirection;
        
        
        public PlayerAnimationHandler(PlayerView playerView, InputSignals inputSignals)
        {
            _playerView = playerView;
            _inputSignals = inputSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputSignals.OnInputTaken += OnInputTaken;
            _inputSignals.OnInputReleased += OnInputReleased;
        }
        
        private void OnInputTaken(InputParams inputParams)
        {
            _moveDirection = inputParams.MoveDirection;
            
            var state = Walk;
            if(state == _currentState) return;
            _playerView.PlayerAnimator.CrossFade(state,0,0);
            _currentState = state;
        }
        
        private void OnInputReleased()
        {
            var state = Idle;
            if(state == _currentState) return;
            _playerView.PlayerAnimator.CrossFade(state,0,0);
            _currentState = state;
        }
        
        private void UnsubscribeEvents()
        {
            _inputSignals.OnInputTaken -= OnInputTaken;
            _inputSignals.OnInputReleased -= OnInputReleased;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
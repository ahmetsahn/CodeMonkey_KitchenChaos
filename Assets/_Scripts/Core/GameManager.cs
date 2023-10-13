using System;
using _Scripts.Enums;
using _Scripts.Signals;

namespace _Scripts.Core
{
    public class GameManager : IDisposable
    {
        private readonly CoreGameSignals _coreGameSignals;
        
        private GameStates _currentGameState;
        
        
        public GameManager(
            CoreGameSignals coreGameSignals)
        {
            _coreGameSignals = coreGameSignals;
            
            _currentGameState = GameStates.Menu;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _coreGameSignals.OnGameStateChanged += OnGameStateChanged;
        }
        
        private void OnGameStateChanged(GameStates gameState)
        {
            _currentGameState = gameState;
            
            switch (_currentGameState)
            {
                case GameStates.Menu:
                    _coreGameSignals.OnResetGame?.Invoke();
                    break;
                case GameStates.Play:
                    _coreGameSignals.OnPlayStarted?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnGameStateChanged -= OnGameStateChanged;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
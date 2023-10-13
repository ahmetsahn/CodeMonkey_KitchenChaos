using System;
using _Scripts.Data.CoreGameData;
using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private Image timerImage;
        
        private CoreGameSignals _coreGameSignals;
        
        private TimerData _timerData;
        
        private float _currentTime;
        
        
        [Inject]
        private void Construct(
            CoreGameSignals coreGameSignals,
            TimerData timerData)
        {
            _coreGameSignals = coreGameSignals;
            _timerData = timerData;
            
            _currentTime = _timerData.Time;
        }
        
        private void Update()
        {
            _currentTime -= Time.deltaTime;
            timerImage.fillAmount = _currentTime / _timerData.Time;
            if (_currentTime <= 0)
            {
                _coreGameSignals.OnGameStateChanged?.Invoke(GameStates.Menu);
            }
        }
    }
}
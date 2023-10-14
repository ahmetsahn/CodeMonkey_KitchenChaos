using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        private UISignals _uiSignals;
        
        private CoreGameSignals _coreGameSignals;
        
        [Inject]
        private void Construct(
            UISignals uiSignals,
            CoreGameSignals coreGameSignals)
        {
            _uiSignals = uiSignals;
            _coreGameSignals = coreGameSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            _coreGameSignals.OnGameStarted?.Invoke();
        }

        private void SubscribeEvents()
        {
            _coreGameSignals.OnGameStarted += OnGameStarted;
            _coreGameSignals.OnPlayStarted += OnPlayStarted;
        }
        
        private void OnGameStarted()
        {
            _uiSignals.OnOpenPanel?.Invoke(UIPanelTypes.MenuPanel, 1);
        }

        private void OnPlayStarted()
        {
            _uiSignals.OnClosePanel?.Invoke(1);
            _uiSignals.OnOpenPanel?.Invoke(UIPanelTypes.PlayPanel, 1);
        }
        
        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnGameStarted -= OnGameStarted;
            _coreGameSignals.OnPlayStarted -= OnPlayStarted;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
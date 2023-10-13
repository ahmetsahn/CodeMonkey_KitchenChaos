using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class UIPanelHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] layers;
        
        private UISignals _uiSignals;
        
        private const string PANELS_PATH = "UIPanels/";
        
        private DiContainer _container;
        
        [Inject]
        private void Construct(UISignals uiSignals, DiContainer container)
        {
            _uiSignals = uiSignals;
            _container = container;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _uiSignals.OnOpenPanel += OnOpenPanel;
            _uiSignals.OnClosePanel += OnClosePanel;
            _uiSignals.OnCloseAllPanels += OnCloseAllPanels;
        }
        
        private void OnOpenPanel(UIPanelTypes panelType, int panelIndex)
        {
            OnClosePanel(panelIndex);
            _container.InstantiatePrefab(Resources.Load<GameObject>(PANELS_PATH + panelType), layers[panelIndex]);
        }

        private void OnClosePanel(int panelIndex)
        {
            if (layers[panelIndex].childCount <= 0) return;

#if UNITY_EDITOR
            DestroyImmediate(layers[panelIndex].GetChild(0).gameObject);
#else
                Destroy(layers[value].GetChild(0).gameObject);
#endif
        }
        
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }

        private void UnsubscribeEvents()
        {
            _uiSignals.OnOpenPanel -= OnOpenPanel;
            _uiSignals.OnClosePanel -= OnClosePanel;
            _uiSignals.OnCloseAllPanels -= OnCloseAllPanels;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
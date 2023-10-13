using System;
using _Scripts.Enums;

namespace _Scripts.Signals
{
    public class UISignals
    {
        public Action<UIPanelTypes, int> OnOpenPanel;
        public Action<int> OnClosePanel;
        public Action OnCloseAllPanels;
    }
}
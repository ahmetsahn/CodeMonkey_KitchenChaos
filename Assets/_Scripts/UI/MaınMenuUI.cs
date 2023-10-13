using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private CoreGameSignals _coreGameSignals;
        
        [Inject]
        public void Construct(CoreGameSignals coreGameSignals)
        {
            _coreGameSignals = coreGameSignals;
        }
        public void OnClickedPlay()
        {
            _coreGameSignals.OnGameStateChanged?.Invoke(GameStates.Play);
        }

        public void OnClickedQuit()
        {
            Application.Quit();
        }
    }

}
using _Scripts.Enums;
using UnityEngine.Events;

namespace _Scripts.Signals
{
    public class CoreGameSignals
    {
        public UnityAction OnGameStarted;
        public UnityAction OnPlayStarted;
        public UnityAction OnResetGame;
        public UnityAction<GameStates> OnGameStateChanged;
        public UnityAction OnSuccessOrder;
        public UnityAction OnFailOrder;
    }
}
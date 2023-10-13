using _Scripts.Keys;
using UnityEngine.Events;

namespace _Scripts.Signals
{
    public class InputSignals
    {
        public UnityAction<InputParams> OnInputTaken;
        public UnityAction OnInputReleased;
        public UnityAction OnInteractTaken;
    }
}
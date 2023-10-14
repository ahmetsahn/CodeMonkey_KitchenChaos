using UnityEngine;

namespace _Scripts.Units.Counter
{
    public abstract class BaseCounterFacade : MonoBehaviour
    {
        public abstract void Interact();
        public abstract bool Select();
        public abstract void Deselect();
    }
}
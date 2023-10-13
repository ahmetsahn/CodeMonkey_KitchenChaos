using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.Units.Counter
{
    public abstract class BaseCounterFacade : MonoBehaviour
    {
        public abstract void Interact();
        public abstract bool Select();
        public abstract void Deselect();
    }
}
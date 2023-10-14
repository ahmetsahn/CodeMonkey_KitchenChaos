using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter
{
    public abstract class BaseCounterView : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectedCounter;
        
        private KitchenObjects _kitchenObjectOwnedByThePlayer;
        
        public GameObject SelectedCounter => selectedCounter;
        
        public KitchenObjects KitchenObjectOwnedByThePlayer
        {
            get => _kitchenObjectOwnedByThePlayer;
            set => _kitchenObjectOwnedByThePlayer = value;
        } 
    }
}
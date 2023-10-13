using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.ClearCounter
{
    public class ClearCounterView : BaseCounterView
    {
        private KitchenObjects _kitchenObjectOnTheCounter;
        public KitchenObjects KitchenObjectOnTheCounter
        {
            get => _kitchenObjectOnTheCounter;
            set => _kitchenObjectOnTheCounter = value;
        }
        
        [SerializeField]
        private Transform kitchenObjectSpawnPositionOnCounter;
        public Transform KitchenObjectSpawnPositionOnCounter => kitchenObjectSpawnPositionOnCounter;
    }
}
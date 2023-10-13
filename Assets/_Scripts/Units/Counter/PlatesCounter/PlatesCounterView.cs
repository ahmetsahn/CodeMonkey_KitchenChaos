using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.PlatesCounter
{
    public class PlatesCounterView : BaseCounterView
    {
        [SerializeField]
        private KitchenObjects _kitchenObjectOnTheCounter;
        public KitchenObjects KitchenObjectOnTheCounter
        {
            get => _kitchenObjectOnTheCounter;
            set => _kitchenObjectOnTheCounter = value;
        }
    }
}
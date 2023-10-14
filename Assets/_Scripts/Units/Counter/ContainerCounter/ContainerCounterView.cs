using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.ContainerCounter
{
    public class ContainerCounterView : BaseCounterView
    {
        [SerializeField]
        private Animator containerCounterAnimator;
        
        public Animator ContainerCounterAnimator
        {
            get => containerCounterAnimator;
        }
        
        [SerializeField]
        private KitchenObjects kitchenObjectOnTheCounter;
        
        public KitchenObjects KitchenObjectOnTheCounter
        {
            get => kitchenObjectOnTheCounter;
            set => kitchenObjectOnTheCounter = value;
        }
    }
}
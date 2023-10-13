using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.ContainerCounter
{
    public class ContainerCounterView : BaseCounterView
    {
        [SerializeField]
        private Animator containerCounterAnimator;
        
        [SerializeField]
        private KitchenObjects kitchenObjectOnTheCounter;
        
        public Animator ContainerCounterAnimator => containerCounterAnimator;
        
        public KitchenObjects KitchenObjectOnTheCounter
        {
            get => kitchenObjectOnTheCounter;
            set => kitchenObjectOnTheCounter = value;
        }
    }
}
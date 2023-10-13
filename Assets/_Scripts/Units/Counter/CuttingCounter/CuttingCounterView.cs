using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.CuttingCounter
{
    public class CuttingCounterView : BaseCounterView
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
        
        
        [SerializeField]
        private Animator cuttingCounterAnimator;
        public Animator CuttingCounterAnimator => cuttingCounterAnimator;
    }
}
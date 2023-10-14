using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Units.Counter.StoveCounter
{
    public class StoveCounterView : BaseCounterView
    {
        [SerializeField]
        private GameObject stoveCounterVisual;

        public GameObject StoveCounterVisual
        {
            get => stoveCounterVisual;
        }
        
        [SerializeField]
        private ParticleSystem fireParticleSystem;

        public ParticleSystem FireParticleSystem
        {
            get => fireParticleSystem;
        }
        
        private KitchenObjects _kitchenObjectOnTheCounter;
        
        public KitchenObjects KitchenObjectOnTheStove
        {
            get => _kitchenObjectOnTheCounter;
            set => _kitchenObjectOnTheCounter = value;
        }
        
        [SerializeField]
        private Transform kitchenObjectSpawnPositionOnCounter;

        public Transform KitchenObjectSpawnPositionOnCounter
        {
            get => kitchenObjectSpawnPositionOnCounter;
        }
    }
}
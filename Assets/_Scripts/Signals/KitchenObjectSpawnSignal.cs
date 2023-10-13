using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Signals
{
    public class KitchenObjectSpawnSignal
    {
        public UnityAction<KitchenObjects,Transform> OnKitchenObjectSpawn;
        public UnityAction<Transform> OnKitchenObjectReturnToPool;
        public UnityAction<Transform, Transform> OnChangeLocationOfKitchenObjects;
    }
}
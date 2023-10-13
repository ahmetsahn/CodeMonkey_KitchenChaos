using System;
using _Scripts.Enums;
using _Scripts.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Signals
{
    public class PlayerSignals
    {
        public UnityAction OnEnableInteractRaycast;
        public UnityAction OnDisableInteractRaycast;
        public UnityAction<KitchenObjectOwnedByThePlayerParams> OnKitchenObjectOwnedByThePlayerChanged;
        public Func<Transform> OnGetKitchenObjectSpawnPositionOnPlayer;
    }
}
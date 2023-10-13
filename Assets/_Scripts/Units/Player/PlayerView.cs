using System;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Units.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Animator playerAnimator;
        
        [SerializeField]
        private Rigidbody playerRigidbody;
        
        [SerializeField]
        private Transform raycastOrigin;
        
        [SerializeField]
        private Transform kitchenObjectSpawnPositionOnPlayer;
        
        [SerializeField]
        private LayerMask countersLayerMask;

        public Transform PlayerTransform => transform;

        public Rigidbody PlayerRigidbody => playerRigidbody;

        public Vector3 RaycastOriginPosition => raycastOrigin.position;

        public LayerMask CountersLayerMask => countersLayerMask;

        public Animator PlayerAnimator => playerAnimator;
        
        private PlayerSignals _playerSignals;
        
        [Inject]
        public void Construct(PlayerSignals playerSignals)
        {
            _playerSignals = playerSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer += OnGetPlayerKitchenObjectPosition;
        }
        
        private Transform OnGetPlayerKitchenObjectPosition()
        {
            return kitchenObjectSpawnPositionOnPlayer;
        }
        
        private void UnsubscribeEvents()
        {
            _playerSignals.OnGetKitchenObjectSpawnPositionOnPlayer -= OnGetPlayerKitchenObjectPosition;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
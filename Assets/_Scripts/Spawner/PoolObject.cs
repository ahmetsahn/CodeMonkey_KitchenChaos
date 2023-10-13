using System;
using UnityEngine;

namespace _Scripts.Spawner
{
    public class PoolObject : MonoBehaviour, IPoolable<PoolObject>
    {
        private Action<PoolObject> _returnToPoolAction;

        private void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Action<PoolObject> returnToPoolAction)
        {
            _returnToPoolAction = returnToPoolAction;
        }

        public void ReturnToPool()
        {
            _returnToPoolAction?.Invoke(this);
        }
    }
}
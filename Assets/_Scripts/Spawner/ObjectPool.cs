using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace _Scripts.Spawner
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private DiContainer _diContainer;
        public ObjectPool(DiContainer diContainer ,GameObject pooledObject, int numToSpawn = 0)
        {
            _diContainer = diContainer;
            prefab = pooledObject;
            Spawn(numToSpawn);
        }

        public ObjectPool(DiContainer diContainer, GameObject pooledObject, Action<T> pullObject, Action<T> pushObject, int numToSpawn = 0)
        {
            _diContainer = diContainer;
            this.prefab = pooledObject;
            this.pullObject = pullObject;
            this.pushObject = pushObject;
            Spawn(numToSpawn);
        }

        private Action<T> pullObject;
        private Action<T> pushObject;
        private Stack<T> pooledObjects = new Stack<T>();
        private GameObject prefab;
        public int pooledCount
        {
            get
            {
                return pooledObjects.Count;
            }
        }

        public T Pull()
        {
            T t;
            if (pooledCount > 0)
                t = pooledObjects.Pop();
            else
                t = _diContainer.InstantiatePrefab(prefab).GetComponent<T>();

            t.gameObject.SetActive(true);
            t.Initialize(Push);
            
            pullObject?.Invoke(t);

            return t;
        }

        public T Pull(Vector3 position)
        {
            var t = Pull();
            t.transform.position = position;
            return t;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            var t = Pull();
            var transform = t.transform;
            transform.position = position;
            transform.rotation = rotation;
            return t;
        }

        public GameObject PullGameObject()
        {
            return Pull().gameObject;
        }

        public GameObject PullGameObject(Transform position)
        {
            var go = Pull().gameObject;
            go.transform.SetParent(position);
            go.transform.localPosition = Vector3.zero;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation)
        {
            var go = Pull().gameObject;
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }

        public void Push(T t)
        {
            pooledObjects.Push(t);
            pushObject?.Invoke(t);
            t.gameObject.SetActive(false);
        }

        private void Spawn(int number)
        {
            for (var i = 0; i < number; i++)
            {
                var t = GameObject.Instantiate(prefab).GetComponent<T>();
                pooledObjects.Push(t);
                t.gameObject.SetActive(false);
            }
        }
    }

    public interface IPool<T>
    {
        T Pull();
        void Push(T t);
    }

    public interface IPoolable<T>
    {
        void Initialize(System.Action<T> returnAction);
        void ReturnToPool();
    }
}
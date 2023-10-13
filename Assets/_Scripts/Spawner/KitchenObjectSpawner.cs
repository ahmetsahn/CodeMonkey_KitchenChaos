using System;
using System.Collections.Generic;
using _Scripts.Data.SpawnerData;
using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using Zenject;

namespace _Scripts.Spawner
{
    public class KitchenObjectSpawner : IDisposable
    {
        private DiContainer _diContainer;
        
        private readonly KitchenObjectSpawnerData _kitchenObjectSpawnerData;
        
        private readonly Dictionary<KitchenObjects,ObjectPool<PoolObject>> _kitchenObjectsDictionary;
        
        private readonly KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        private static ObjectPool<PoolObject> _breadPool;
        private static ObjectPool<PoolObject> _uncookedMeatPool;
        private static ObjectPool<PoolObject> _cookedMeatPool;
        private static ObjectPool<PoolObject> _burnedMeatPool;
        private static ObjectPool<PoolObject> _tomatoPool;
        private static ObjectPool<PoolObject> _slicedTomatoPool;
        private static ObjectPool<PoolObject> _cabbagePool;
        private static ObjectPool<PoolObject> _slicedCabbagePool;
        private static ObjectPool<PoolObject> _cheesePool;
        private static ObjectPool<PoolObject> _slicedCheesePool;
        private static ObjectPool<PoolObject> _platePool;
        
        public KitchenObjectSpawner(
            KitchenObjectSpawnerData kitchenObjectSpawnerData, 
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal,
            DiContainer diContainer)
        {
            _diContainer = diContainer;
            
            Debug.Log(_diContainer);
            
            _kitchenObjectSpawnerData = kitchenObjectSpawnerData;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;

            _kitchenObjectsDictionary = new Dictionary<KitchenObjects, ObjectPool<PoolObject>>();
            
            _breadPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.Bread);
            _uncookedMeatPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.UncookedMeat);
            _cookedMeatPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.CookedMeat);
            _burnedMeatPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.BurnedMeat);
            _tomatoPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.Tomato);
            _slicedTomatoPool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.SlicedTomato);
            _cabbagePool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.Cabbage);
            _slicedCabbagePool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.SlicedCabbage);
            _cheesePool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.Cheese);
            _slicedCheesePool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.SlicedCheese);
            _platePool = new ObjectPool<PoolObject>(_diContainer,_kitchenObjectSpawnerData.Plate);
            
            Initialize();
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _kitchenObjectSpawnSignal.OnKitchenObjectSpawn += Spawn;
            _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool += ReturnToPool;
            _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects += ChangeLocationOfKitchenObjects;
        }
        
        public void Initialize()
        {
            _kitchenObjectsDictionary.Add(KitchenObjects.Bread, _breadPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.UncookedMeat, _uncookedMeatPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.CookedMeat, _cookedMeatPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.BurnedMeat, _burnedMeatPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.Tomato, _tomatoPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.SlicedTomato, _slicedTomatoPool);
            _kitchenObjectsDictionary.Add(KitchenObjects.Cabbage, _cabbagePool);
            _kitchenObjectsDictionary.Add(KitchenObjects.SlicedCabbage, _slicedCabbagePool);
            _kitchenObjectsDictionary.Add(KitchenObjects.Cheese, _cheesePool);
            _kitchenObjectsDictionary.Add(KitchenObjects.SlicedCheese, _slicedCheesePool);
            _kitchenObjectsDictionary.Add(KitchenObjects.Plate, _platePool);
        }

        public void Spawn(KitchenObjects kitchenObjects,Transform position)
        {
            _kitchenObjectsDictionary[kitchenObjects].PullGameObject(position);
        }
        
        private void ChangeLocationOfKitchenObjects(Transform firstPosition, Transform secondPosition)
        {
            var childCount = firstPosition.childCount;
            
            for (var i = 0; i < childCount; i++)
            {
                var child = firstPosition.GetChild(0);
                child.SetParent(secondPosition);
                child.localPosition = Vector3.zero;
            }
        }
     
        public void ReturnToPool(Transform position)
        {
            var childCount = position.childCount;
            
            for (var i = 0; i < childCount; i++)
            {
                var child = position.GetChild(0);
                child.SetParent(null);
                child.gameObject.SetActive(false);
            }
        }
        
        private void UnsubscribeEvents()
        {
            _kitchenObjectSpawnSignal.OnKitchenObjectSpawn -= Spawn;
            _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool -= ReturnToPool;
            _kitchenObjectSpawnSignal.OnChangeLocationOfKitchenObjects -= ChangeLocationOfKitchenObjects;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
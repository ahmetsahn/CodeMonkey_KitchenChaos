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
        private readonly DiContainer _diContainer;
        
        private readonly KitchenObjectSpawnerData _kitchenObjectSpawnerData;
        
        private Dictionary<KitchenObjects,ObjectPool<PoolObject>> _kitchenObjectsDictionary;
        
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
            _kitchenObjectSpawnerData = kitchenObjectSpawnerData;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
            
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
            
            _kitchenObjectsDictionary = new Dictionary<KitchenObjects, ObjectPool<PoolObject>>
            {
                { KitchenObjects.Bread, _breadPool },
                { KitchenObjects.UncookedMeat, _uncookedMeatPool },
                { KitchenObjects.CookedMeat, _cookedMeatPool },
                { KitchenObjects.BurnedMeat, _burnedMeatPool },
                { KitchenObjects.Tomato, _tomatoPool },
                { KitchenObjects.SlicedTomato, _slicedTomatoPool },
                { KitchenObjects.Cabbage, _cabbagePool },
                { KitchenObjects.SlicedCabbage, _slicedCabbagePool },
                { KitchenObjects.Cheese, _cheesePool },
                { KitchenObjects.SlicedCheese, _slicedCheesePool },
                { KitchenObjects.Plate, _platePool }
            };
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
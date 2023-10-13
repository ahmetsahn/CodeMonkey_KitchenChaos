using System;
using _Scripts.Data.CoreGameData;
using _Scripts.Data.CountersData;
using _Scripts.Data.InputData;
using _Scripts.Data.KitchenObjectsData;
using _Scripts.Data.PlayerData;
using _Scripts.Data.SpawnerData;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private CoreGameData coreGameData;
        
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private CountersData countersData;
        
        [SerializeField]
        private InputData inputData;
        
        [SerializeField]
        private KitchenObjects kitchenObjectsData;
        
        [SerializeField] 
        private KitchenObjectSpawnerData kitchenObjectSpawnerData;
        
        [Serializable]
        public class CoreGameData
        {
            public TimerData TimerData;
            public OrderData OrderData;
        }
        
        [Serializable]
        public class PlayerData
        {
            public PlayerMovementData PlayerMovementData;
            public PlayerRotationData PlayerRotationData;
            public PlayerInteractData PlayerInteractData;
        }
        
        [Serializable]
        public class CountersData
        {
            public StoveCounterData StoveCounterData;
        }
        
        [Serializable]
        public class KitchenObjects
        {
            public KitchenObjectsData KitchenObjectsData;
        }
        public override void InstallBindings()
        {
            CoreGameDataBindings();
            PlayerDataBindings();
            CountersDataBindings();
            KitchenObjectsDataBindings();
            InputDataBindings();
            SpawnerDataBindings();
        }
        
        private void CoreGameDataBindings()
        {
            Container.BindInstance(coreGameData.TimerData).IfNotBound();
            Container.BindInstance(coreGameData.OrderData).IfNotBound();
        }
        
        private void PlayerDataBindings()
        {
            Container.BindInstance(playerData.PlayerMovementData).IfNotBound();
            Container.BindInstance(playerData.PlayerRotationData).IfNotBound();
            Container.BindInstance(playerData.PlayerInteractData).IfNotBound();
        }
        
        private void CountersDataBindings()
        {
            Container.BindInstance(countersData.StoveCounterData).IfNotBound();
        }
        
        private void KitchenObjectsDataBindings()
        {
            Container.BindInstance(kitchenObjectsData.KitchenObjectsData).IfNotBound();
        }
        
        private void InputDataBindings()
        {
            Container.BindInstance(inputData).IfNotBound();
        }
        
        private void SpawnerDataBindings()
        {
            Container.BindInstance(kitchenObjectSpawnerData).IfNotBound();
        }
    }
}
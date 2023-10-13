using _Scripts.Core;
using _Scripts.Keys;
using _Scripts.Signals;
using _Scripts.Spawner;
using Zenject;

namespace _Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GameManagerBindings();
            SpawnerBindings();
            InputManagerBindings();
            SignalBindings();
            ListParamsBindings();
        }
        
        private void GameManagerBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
        }
        
        private void ListParamsBindings()
        {
            Container.BindInterfacesAndSelfTo<ListParams>().AsSingle();
        }
        
        private void SignalBindings()
        {
            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<CounterSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();
            Container.Bind<KitchenObjectSpawnSignal>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<ListSignals>().AsSingle();
            Container.Bind<PlateSignals>().AsSingle();
        }
        
        private void InputManagerBindings()
        {
            Container.BindInterfacesTo<InputManager>().AsSingle().NonLazy();
        }
        
        private void SpawnerBindings()
        {
            Container.BindInterfacesAndSelfTo<KitchenObjectSpawner>().AsSingle().NonLazy();
        }
    }
}
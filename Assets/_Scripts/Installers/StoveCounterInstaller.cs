using _Scripts.Units.Counter.StoveCounter;
using Zenject;

namespace _Scripts.Installers
{
    public class StoveCounterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StoveCounterVisual>().AsSingle();
        }
    }
}
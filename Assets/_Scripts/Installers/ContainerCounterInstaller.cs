using _Scripts.Units.Counter.ContainerCounter;
using Zenject;

namespace _Scripts.Installers
{
    public class ContainerCounterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ContainerCounterAnimationHandler>().AsSingle();
        }
    }
}
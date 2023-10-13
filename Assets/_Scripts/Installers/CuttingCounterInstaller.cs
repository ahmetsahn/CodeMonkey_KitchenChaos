using _Scripts.Units.Counter.CuttingCounter;
using Zenject;

namespace _Scripts.Installers
{
    public class CuttingCounterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CuttingCounterAnimationHandler>().AsSingle();
        }
    }
}
using _Scripts.Units.Player;
using Zenject;

namespace _Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerRotationHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInteractHandler>().AsSingle();
        }
    }
}
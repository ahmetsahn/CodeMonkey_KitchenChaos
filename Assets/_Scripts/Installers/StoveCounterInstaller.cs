using _Scripts.Units.Counter.StoveCounter;
using UnityEngine;
using Zenject;

public class StoveCounterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StoveCounterVisual>().AsSingle();
    }
}
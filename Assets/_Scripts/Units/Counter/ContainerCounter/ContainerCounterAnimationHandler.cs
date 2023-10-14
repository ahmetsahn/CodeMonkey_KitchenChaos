using UnityEngine;

namespace _Scripts.Units.Counter.ContainerCounter
{
    public class ContainerCounterAnimationHandler
    {
        private readonly ContainerCounterView _containerCounterView;
        
        private static readonly int Open = Animator.StringToHash("Open");
        
        public ContainerCounterAnimationHandler(
            ContainerCounterView containerCounterView)
        {
            _containerCounterView = containerCounterView;
        }

        public void PlayOpenAnimation()
        {
            _containerCounterView.ContainerCounterAnimator.SetTrigger(Open);
        }
    }
}
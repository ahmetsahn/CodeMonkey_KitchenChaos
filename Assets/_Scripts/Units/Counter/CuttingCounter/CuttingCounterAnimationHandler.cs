using UnityEngine;

namespace _Scripts.Units.Counter.CuttingCounter
{
    public class CuttingCounterAnimationHandler
    {
        private readonly CuttingCounterView _cuttingCounterView;
        
        private static readonly int Default = Animator.StringToHash("Default");
        private static readonly int Cut = Animator.StringToHash("Cut");

        public const float CUT_ANIMATION_TIME = 0.6f;

        public readonly WaitForSeconds _cutAnimationWaitForSeconds;
        
        public CuttingCounterAnimationHandler(
            CuttingCounterView cuttingCounterView)
        {
            _cuttingCounterView = cuttingCounterView;
            
            _cutAnimationWaitForSeconds = new WaitForSeconds(CUT_ANIMATION_TIME);
        }

        public void PlayCutAnimation()
        {
            _cuttingCounterView.CuttingCounterAnimator.CrossFade(Cut,0,0);
        }
        
        public void StopCutAnimation()
        {
            _cuttingCounterView.CuttingCounterAnimator.CrossFade(Default,0,0);
        }
        
    }
}
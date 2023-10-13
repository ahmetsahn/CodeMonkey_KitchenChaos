namespace _Scripts.Units.Counter.StoveCounter
{
    public class StoveCounterVisual
    {
        private readonly StoveCounterView _stoveCounterView;
        
        public StoveCounterVisual(StoveCounterView stoveCounterView)
        {
            _stoveCounterView = stoveCounterView;
        }
        
        public void PutKitchenObjectOnTheStove()
        {
            HandleStoveVisual();
        }
        
        public void TakeKitchenObjectFromTheStove()
        {
            ResetVisual();
        }
        
        private void PlayFireParticle()
        {
            _stoveCounterView.FireParticleSystem.Play();
        }
        
        private void StopFireParticle()
        {
            _stoveCounterView.FireParticleSystem.Stop();
        }
        
        private void SetTrueStoveCounterVisual()
        {
            _stoveCounterView.StoveCounterVisual.SetActive(true);
        }
        
        private void SetFalseStoveCounterVisual()
        {
            _stoveCounterView.StoveCounterVisual.SetActive(false);
        }
        
        private void HandleStoveVisual()
        {
            SetTrueStoveCounterVisual();
            PlayFireParticle();
        }
        
        private void ResetVisual()
        {
            StopFireParticle();
            SetFalseStoveCounterVisual();
        }
    }
}
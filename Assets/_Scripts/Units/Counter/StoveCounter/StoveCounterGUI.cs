using _Scripts.Data.CountersData;
using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

namespace _Scripts.Units.Counter.StoveCounter
{
    public class StoveCounterGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject stoveCounterGUI;
        
        [SerializeField]
        private Image stoveCounterFillImage;
        
        [SerializeField]
        private Image dangerImage;
        
        private StoveCounterData _stoveCounterData;
        
        private StoveCounterView _stoveCounterView;
        
        private KitchenObjectSpawnSignal _kitchenObjectSpawnSignal;
        
        
        [Inject]
        public void Construct(
            StoveCounterView stoveCounterView, 
            KitchenObjectSpawnSignal kitchenObjectSpawnSignal,
            StoveCounterData stoveCounterData)
        {
            _stoveCounterView = stoveCounterView;
            _kitchenObjectSpawnSignal = kitchenObjectSpawnSignal;
            _stoveCounterData = stoveCounterData;
        }
       
        public void PutKitchenObjectOnTheStove()
        {
            stoveCounterGUI.SetActive(true);
            HandleStoveGUI();
        }
        
        public void TakeKitchenObjectFromTheStove()
        {
            ResetGUI();
            SetFalseDangerImage();
            SetFalseStoveCounterGUI();
        }
        
        private void HandleStoveGUI()
        {
            stoveCounterFillImage.DOFillAmount(1, _stoveCounterData.CookingTime)
                .SetEase(Ease.Linear).onComplete += () =>
            {
                _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.Invoke(
                    _stoveCounterView.KitchenObjectSpawnPositionOnCounter);

                _stoveCounterView.KitchenObjectOnTheStove = KitchenObjects.CookedMeat;
                    
                _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.Invoke(
                    _stoveCounterView.KitchenObjectOnTheStove,
                    _stoveCounterView.KitchenObjectSpawnPositionOnCounter);
                
                ResetFillAmount();
                SetTrueDangerImage();
                DangerLoopAnimation();

                stoveCounterFillImage.DOFillAmount(1, _stoveCounterData.CookingTime)
                    .SetEase(Ease.Linear).onComplete += () =>
                {
                    _kitchenObjectSpawnSignal.OnKitchenObjectReturnToPool?.Invoke(
                        _stoveCounterView.KitchenObjectSpawnPositionOnCounter);

                    _stoveCounterView.KitchenObjectOnTheStove = KitchenObjects.BurnedMeat;
                    
                    _kitchenObjectSpawnSignal.OnKitchenObjectSpawn?.Invoke(
                        _stoveCounterView.KitchenObjectOnTheStove,
                        _stoveCounterView.KitchenObjectSpawnPositionOnCounter);
                    
                    SetFalseStoveCounterGUI();
                };
            };
        }
        
        private void SetTrueDangerImage()
        {
            dangerImage.gameObject.SetActive(true);
        }
        
        private void SetFalseDangerImage()
        {
            dangerImage.gameObject.SetActive(false);
        }
        
        private void DangerLoopAnimation()
        {
            var duration = _stoveCounterData.CookingTime / _stoveCounterData.Danger_Image_Animation_Loop_Count;
            dangerImage.DOColor(Color.red, duration).SetLoops(_stoveCounterData.Danger_Image_Animation_Loop_Count, LoopType.Yoyo);
        }
        private void SetFalseStoveCounterGUI()
        {
            stoveCounterGUI.SetActive(false);
        }
        
        private void ResetFillAmount()
        {
            stoveCounterFillImage.fillAmount = 0;
        }

        private void ResetDangerImageColor()
        {
            dangerImage.color = Color.white;
        }
        
        private void ResetGUI()
        {
            ResetFillAmount();
            ResetDangerImageColor();
            DOTween.Kill(stoveCounterFillImage);
            DOTween.Kill(dangerImage);
        }
    }
}
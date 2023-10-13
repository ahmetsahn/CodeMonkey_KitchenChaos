using System;
using UnityEngine;
using DG.Tweening;

namespace _Scripts.Units.Counter.DeliveryCounter
{
    public class DeliveryCounterGUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup successCanvasGroup;
        
        [SerializeField]
        private GameObject successCanvas;
        
        [SerializeField]
        private CanvasGroup failCanvasGroup;
        
        [SerializeField]
        private GameObject failCanvas;

        
        public void HandleDeliveryCounterSuccessGUI()
        {
            ResetSuccessCanvasGroupAlpha();
            SetTrueSuccessCanvas();
            SetZeroSuccessCanvasGroupAlpha();
        }
        
        public void HandleDeliveryCounterFailGUI()
        {
            ResetFailCanvasGroupAlpha();
            SetTrueFailCanvas();
            SetZeroFailCanvasGroupAlpha();
        }
        private void SetZeroSuccessCanvasGroupAlpha()
        {
            successCanvasGroup.DOFade(0, 3).OnComplete(ResetDeliveryCounterSuccessGUI);
        }
        
        private void SetZeroFailCanvasGroupAlpha()
        {
            failCanvasGroup.DOFade(0, 3).OnComplete(ResetDeliveryCounterSuccessGUI);
        }
        
        private void ResetSuccessCanvasGroupAlpha()
        {
            successCanvasGroup.alpha = 1;
            DOTween.Kill(successCanvasGroup);
        }
        
        private void ResetFailCanvasGroupAlpha()
        {
            failCanvasGroup.alpha = 1;
            DOTween.Kill(failCanvasGroup);
        }

        private void SetTrueSuccessCanvas()
        {
            successCanvas.SetActive(true);
        }
        
        private void SetTrueFailCanvas()
        {
            failCanvas.SetActive(true);
        }
        
        private void SetFalseSuccessCanvas()
        {
            successCanvas.SetActive(false);
        }
        
        private void SetFalseFailCanvas()
        {
            failCanvas.SetActive(false);
        }
        
        private void ResetDeliveryCounterSuccessGUI()
        {
            SetFalseSuccessCanvas();
            ResetSuccessCanvasGroupAlpha();
        }
        
        private void ResetDeliveryCounterFailGUI()
        {
            SetFalseFailCanvas();
            ResetFailCanvasGroupAlpha();
        }
    }
}
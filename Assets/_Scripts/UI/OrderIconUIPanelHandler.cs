using System;
using System.Collections.Generic;
using _Scripts.Data.CoreGameData;
using _Scripts.Keys;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class OrderIconUIPanelHandler : MonoBehaviour
    {
        [SerializeField]
        private Image[] orderIconImages;
        
        [SerializeField]
        private Sprite[] orderIconSprites;
        
        private readonly List<Sprite> _usedSprites = new List<Sprite>();

        private OrderData _orderData;
        
        private ListSignals _listSignals;
        
        private CoreGameSignals _coreGameSignals;
        
        [Inject]
        private void Construct(
            CoreGameSignals coreGameSignals,
            OrderData orderData,
            ListSignals listSignals)
        {
            _coreGameSignals = coreGameSignals;
            _orderData = orderData;
            _listSignals = listSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void Start()
        {
            SetOrderIconImages();
        }
        
        private void SubscribeEvents()
        {
            _coreGameSignals.OnSuccessOrder += OnSuccessOrder;
        }
        
        private void OnSuccessOrder()
        {
            ResetUsedSprites();
            SetOrderIconImages();
        }
        
        private void SetOrderIconImages()
        {
            foreach (var image in orderIconImages)
            {
                var randomSprite = orderIconSprites[UnityEngine.Random.Range(0, orderIconSprites.Length)];
                while (_usedSprites.Contains(randomSprite))
                {
                    randomSprite = orderIconSprites[UnityEngine.Random.Range(0, orderIconSprites.Length)];
                }
                _usedSprites.Add(randomSprite);
                _listSignals.OnAddToOrderList?.Invoke(_orderData.OrderDictionary[randomSprite]);
                image.sprite = randomSprite;
            }
        }
        
        private void ResetUsedSprites()
        {
            _usedSprites.Clear();
        }
        
        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnSuccessOrder -= OnSuccessOrder;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Signals;

namespace _Scripts.Keys
{
    public class ListParams : IDisposable
    {
        private List<KitchenObjects> _kitchenObjectsListOnThePlane = new();
        private List<KitchenObjects> _orderList = new();

        private readonly CoreGameSignals _coreGameSignals;
        
        private readonly ListSignals _listSignals = new();
        
        
        public ListParams(
            CoreGameSignals coreGameSignals,
            ListSignals listSignals)
        {
            _coreGameSignals = coreGameSignals;
            _listSignals = listSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _coreGameSignals.OnSuccessOrder += OnSuccessOrder;
            _coreGameSignals.OnFailOrder += OnFailOrder;
            _listSignals.OnAddToOrderList += OnAddToOrderList;
            _listSignals.OnAddToPlaneList += OnAddToPlaneList;
            _listSignals.OnIsOrderListAndPlaneListEqual += OnIsOrderListAndPlaneListEqual;
        }
        
        private void OnSuccessOrder()
        {
            ResetKitchenObjectsListOnThePlane();
            ResetOrderList();
        }
        
        private void OnFailOrder()
        {
            ResetKitchenObjectsListOnThePlane();
        }
        
        public void OnAddToOrderList(KitchenObjects kitchenObject)
        {
            _orderList.Add(kitchenObject);
        }
        
        public void OnAddToPlaneList(KitchenObjects kitchenObject)
        {
            _kitchenObjectsListOnThePlane.Add(kitchenObject);
        }
        
        public bool OnIsOrderListAndPlaneListEqual()
        {
            _kitchenObjectsListOnThePlane.Sort();
            _orderList.Sort();
            if (_kitchenObjectsListOnThePlane.Count != _orderList.Count) return false;
            return !_kitchenObjectsListOnThePlane.Where((t, i) => t != _orderList[i]).Any();
        }
        
        public void ResetKitchenObjectsListOnThePlane()
        {
            _kitchenObjectsListOnThePlane.Clear();
        }
        
        public void ResetOrderList()
        {
            _orderList.Clear();
        }

        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnSuccessOrder -= OnSuccessOrder;
            _coreGameSignals.OnFailOrder -= OnFailOrder;
            _listSignals.OnAddToOrderList -= OnAddToOrderList;
            _listSignals.OnAddToPlaneList -= OnAddToPlaneList;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
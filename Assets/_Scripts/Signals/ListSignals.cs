using System;
using _Scripts.Enums;
using UnityEngine.Events;

namespace _Scripts.Signals
{
    public class ListSignals
    {
        public UnityAction<KitchenObjects> OnAddToOrderList;
        public UnityAction<KitchenObjects> OnAddToPlaneList;
        public Func<bool> OnIsOrderListAndPlaneListEqual;
    }
}
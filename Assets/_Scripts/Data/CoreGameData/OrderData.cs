using System;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Scripts.Data.CoreGameData
{
    [Serializable]
    public class OrderData
    {
        public SerializedDictionary<Sprite, KitchenObjects> OrderDictionary = new();
    }
}
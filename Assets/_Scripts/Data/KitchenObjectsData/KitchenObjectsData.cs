using System;
using System.Collections.Generic;
using _Scripts.Enums;
using UnityEngine.Rendering;

namespace _Scripts.Data.KitchenObjectsData
{
    [Serializable]
    public class KitchenObjectsData
    {
        public List<KitchenObjects> SliceableKitchenObjectsList = new();
        public List<KitchenObjects> CookableKitchenObjectsList = new();
        public List<KitchenObjects> PlateableKitchenObjectsList = new();
        public SerializedDictionary<KitchenObjects, KitchenObjects> SlicedKitchenObjectsDictionary = new();
    }
}
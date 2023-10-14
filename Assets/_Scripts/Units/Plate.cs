using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Units
{
    public class Plate : MonoBehaviour
    {
        private readonly Dictionary<KitchenObjects, GameObject> _plateIconsDictionary = new();
        
        [SerializeField] private GameObject breadIcon;
        [SerializeField] private GameObject cookedMeatIcon;
        [SerializeField] private GameObject slicedTomato;
        [SerializeField] private GameObject slicedCabbage;
        [SerializeField] private GameObject slicedCheeseIcon;

        private PlateSignals _plateSignals;
        

        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            _plateIconsDictionary.Add(KitchenObjects.Bread, breadIcon);
            _plateIconsDictionary.Add(KitchenObjects.CookedMeat, cookedMeatIcon);
            _plateIconsDictionary.Add(KitchenObjects.SlicedTomato, slicedTomato);
            _plateIconsDictionary.Add(KitchenObjects.SlicedCabbage, slicedCabbage);
            _plateIconsDictionary.Add(KitchenObjects.SlicedCheese, slicedCheeseIcon);
        }

        [Inject]
        private void Construct(PlateSignals plateSignals)
        {
            _plateSignals = plateSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _plateSignals.OnSetActivePlateIcon += OnSetActivePlateIcon;
        }
        
        private void OnSetActivePlateIcon(KitchenObjects kitchenObject)
        {
            _plateIconsDictionary[kitchenObject].SetActive(true);
        }
        
        private void SetDefaultPlateIcons()
        {
            foreach (var plateIcon in _plateIconsDictionary)
            {
                plateIcon.Value.SetActive(false);
            }
        }
        
        private void UnsubscribeEvents()
        {
            _plateSignals.OnSetActivePlateIcon -= OnSetActivePlateIcon;
        }
        private void OnDisable()
        {
            SetDefaultPlateIcons();
            UnsubscribeEvents();
        }
    }
}
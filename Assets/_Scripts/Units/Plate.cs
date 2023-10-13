using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Signals;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace _Scripts.Units
{
    public class Plate : MonoBehaviour
    {
        private readonly Dictionary<KitchenObjects, GameObject> PlateIconsDictionary = new();
        [SerializeField] private GameObject BreadIcon;
        [SerializeField] private GameObject CookedMeatIcon;
        [SerializeField] private GameObject SlicedTomato;
        [SerializeField] private GameObject SlicedCabbage;
        [SerializeField] private GameObject SlicedCheeseIcon;

        private PlateSignals _plateSignals;
        

        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            PlateIconsDictionary.Add(KitchenObjects.Bread, BreadIcon);
            PlateIconsDictionary.Add(KitchenObjects.CookedMeat, CookedMeatIcon);
            PlateIconsDictionary.Add(KitchenObjects.SlicedTomato, SlicedTomato);
            PlateIconsDictionary.Add(KitchenObjects.SlicedCabbage, SlicedCabbage);
            PlateIconsDictionary.Add(KitchenObjects.SlicedCheese, SlicedCheeseIcon);
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
            PlateIconsDictionary[kitchenObject].SetActive(true);
        }
        
        private void SetDefaultPlateIcons()
        {
            foreach (var plateIcon in PlateIconsDictionary)
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
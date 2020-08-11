using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.CellControllers.SubControllers
{
    public class GroceryItemEditController : MonoBehaviour
    {
        public GroceryItemPrefabController viewController;
        public TMP_InputField nameText;
        public TMP_InputField countText;
        public TMP_InputField priceText;
        public Button deleteButton;
        public Button saveButton;

        public void Configure(GroceryItemPrefabController viewController, GroceryItem item)
        {
            this.viewController = viewController;
            nameText.text = item.itemName;
            countText.text = item.itemCount;
            priceText.text = item.itemPrice;
        }

        private void Awake()
        {
            deleteButton.onClick.AddListener(viewController.OnDeleteButtonClicked);
            saveButton.onClick.AddListener(viewController.OnSaveButtonClicked);
        }

        private void OnDestroy()
        {
            deleteButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
        }
    }
}
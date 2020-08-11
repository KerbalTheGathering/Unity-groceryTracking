using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.CellControllers.SubControllers
{
    public class GroceryItemViewController : MonoBehaviour
    {
        public GroceryItemPrefabController viewController;
        public TMP_Text nameText;
        public TMP_Text countText;
        public TMP_Text priceText;
        public Button editButton;

        public void Configure(GroceryItemPrefabController viewController, GroceryItem item)
        {
            this.viewController = viewController;
            nameText.text = item.itemName;
            countText.text = item.itemCount;
            priceText.text = "$ " + item.itemPrice;
        }

        private void Awake()
        {
            editButton.onClick.AddListener(viewController.OnEditButtonClicked);
        }

        private void OnDestroy()
        {
            editButton.onClick.RemoveAllListeners();
        }
    }
}
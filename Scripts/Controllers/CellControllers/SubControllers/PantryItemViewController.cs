using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.CellControllers.SubControllers
{
    public class PantryItemViewController : MonoBehaviour
    {
        public PantryItemPrefabController viewController;
        public TMP_Text nameText;
        public TMP_Text quantityText;
        public Button editButton;
        
        public void Configure(PantryItemPrefabController viewController, PantryItem pantryItem)
        {
            this.viewController = viewController;
            nameText.text = pantryItem.itemName;
            quantityText.text = pantryItem.quantity.ToString("F2");
        }
        
        private void Awake()
        {
            nameText.text = viewController.PantryItem.itemName;
            quantityText.text = viewController.PantryItem.quantity.ToString("F2");
            editButton.onClick.AddListener(viewController.OnEditButtonClicked);
        }

        private void OnDestroy()
        {
            editButton.onClick.RemoveAllListeners();
        }
    }
}
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.CellControllers.SubControllers
{
    public class PantryItemEditController : MonoBehaviour
    {
        public PantryItemPrefabController viewController;
        public TMP_InputField nameText;
        public TMP_InputField quantityText;
        public Button deleteButton;
        public Button saveButton;

        public void Configure(PantryItemPrefabController viewController, PantryItem item)
        {
            this.viewController = viewController;
            nameText.text = item.itemName;
            quantityText.text = item.quantity.ToString("F2");
        }
        
        private void Awake()
        {
            deleteButton.onClick.AddListener(viewController.OnEditButtonClicked);
            saveButton.onClick.AddListener(viewController.OnSaveButtonClicked);
        }

        private void OnDestroy()
        {
            deleteButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
        }
    }
}
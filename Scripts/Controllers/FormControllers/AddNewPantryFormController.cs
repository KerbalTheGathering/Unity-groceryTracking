using System;
using DefaultNamespace;
using rtome.Scripts.Services;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.FormControllers
{
    public class AddNewPantryFormController : MonoBehaviour
    {
        [Title("Form Components:")]
        public Button addNewButton;
        public TMP_InputField itemNameInputField;
        public TMP_InputField itemQuantityInputField;
        [Title("Configuration:")]
        public MainServiceController mainController;
        
        public EventHandler<PantryItemAddedEventArgs> NotifyItemAdded { get; set; }
        
        public void OnAddNewButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(itemNameInputField.text)
                || string.IsNullOrWhiteSpace(itemQuantityInputField.text))
            {
                //Set validation responses
                //Turn inputs red, etc.
                return;
            }

            var newItem = new PantryItem(
                itemNameInputField.text,
                float.Parse(itemQuantityInputField.text)
            );
            WebDataAccessor.PostPantryInventoryItem(newItem);
            mainController.pantryInventory.Add(newItem);
            ClearFormData();
            NotifyItemAdded?.Invoke(this, new PantryItemAddedEventArgs(newItem));
        }
        
        private void ClearFormData()
        {
            itemNameInputField.text = string.Empty;
            itemQuantityInputField.text = string.Empty;
        }

        private void Start()
        {
            addNewButton.onClick.AddListener(OnAddNewButtonClicked);
        }

        private void OnDestroy()
        {
            addNewButton.onClick.RemoveAllListeners();
        }
    }
}
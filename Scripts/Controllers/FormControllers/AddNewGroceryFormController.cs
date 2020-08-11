using System;
using DefaultNamespace;
using rtome.Scripts.Services;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.FormControllers
{
    public class AddNewGroceryFormController : MonoBehaviour
    {
        [Title("Form Components:")]
        public Button addNewButton;
        public TMP_InputField itemNameInputField;
        public TMP_InputField itemQuantityInputField;
        public TMP_InputField itemPricePerInputField;
        [Title("Configuration:")]
        public MainServiceController mainController;
        
        public EventHandler<GroceryItemAddedEventArgs> NotifyItemAdded { get; set; }
        
        public void OnAddNewButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(itemNameInputField.text)
                || string.IsNullOrWhiteSpace(itemQuantityInputField.text))
            {
                //Set validation responses
                //Turn inputs red, etc.
                return;
            }
            var newItem = new GroceryItem(
                itemNameInputField.text
                , itemQuantityInputField.text
                , itemPricePerInputField.text);
            WebDataAccessor.PostGroceryItem(newItem);
            mainController.groceryListInventory.Add(newItem);
            ClearFormData();
            NotifyItemAdded?.Invoke(this, new GroceryItemAddedEventArgs(newItem));
        }
        
        private void ClearFormData()
        {
            itemNameInputField.text = string.Empty;
            itemQuantityInputField.text = string.Empty;
            itemPricePerInputField.text = string.Empty;
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
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using EnhancedUI.EnhancedScroller;
using JetBrains.Annotations;
using rtome.Scripts.Controllers.Body;
using rtome.Scripts.Controllers.CellControllers.SubControllers;
using rtome.Scripts.Services;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.CellControllers
{
    public class GroceryItemPrefabController : EnhancedScrollerCellView, INotifyPropertyChanged
    {
        private GroceryListViewController _viewController;
        [ShowInInspector, EnableGUI] private GroceryItem _groceryItem;
        [ShowInInspector, EnableGUI] private bool _isEditing;
        public GroceryItemViewController viewConfiguration;
        public GroceryItemEditController editConfiguration;

        public GroceryItem GroceryItem
        {
            get => _groceryItem;
            set
            {
                if (_groceryItem == value)
                    return;
                _groceryItem = value;
                NotifyPropertyChanged(nameof(GroceryItem));
            }
        }
        
        public EventHandler<GroceryItemAddedEventArgs> NotifyEditGroceryItem { get; set; }
        public EventHandler<GroceryItemAddedEventArgs> NotifyDeleteGroceryItem { get; set; }

        public void SetData(GroceryListViewController viewController, GroceryItem item)
        {
            _viewController = viewController;
            GroceryItem = item;
            if (_isEditing)
            {
                editConfiguration.Configure(this, GroceryItem);
                ConfigureForEdit();
            }
            else
            {
                viewConfiguration.Configure(this, GroceryItem);
                ConfigureForView();
            }
        }

        public void OnEditButtonClicked()
        {
            editConfiguration.Configure(this, GroceryItem);
            ConfigureForEdit();
            /*
            itemEditButton.gameObject.SetActive(false);
            itemDeleteButton.gameObject.SetActive(true);
            NotifyEditGroceryItem?.Invoke(this, new GroceryItemAddedEventArgs(_groceryItem));
            */
        }

        public void OnDeleteButtonClicked()
        {
            WebDataAccessor.RemoveGroceryItem(GroceryItem);
            _viewController.ReloadData();
            /*
            itemEditButton.gameObject.SetActive(true);
            itemDeleteButton.gameObject.SetActive(false);
            NotifyDeleteGroceryItem?.Invoke(this, new GroceryItemAddedEventArgs(_groceryItem));
            */
        }

        public void OnSaveButtonClicked()
        {
            viewConfiguration.viewController = this;
            GroceryItem = new GroceryItem(GroceryItem.id
                , editConfiguration.nameText.text
                , editConfiguration.countText.text
                , float.Parse(editConfiguration.priceText.text).ToString("F2"));
            WebDataAccessor.PostGroceryItem(GroceryItem);
            _viewController.mainController.LoadGroceryListFromWeb();
            ConfigureForView();
        }

        private void Start()
        {
        }

        private void OnDestroy()
        {
        }

        private void ConfigureForEdit()
        {
            editConfiguration.gameObject.SetActive(true);
            viewConfiguration.gameObject.SetActive(false);
            editConfiguration.Configure(this, GroceryItem);
            _isEditing = true;
        }

        private void ConfigureForView()
        {
            editConfiguration.gameObject.SetActive(false);
            viewConfiguration.gameObject.SetActive(true);
            viewConfiguration.Configure(this, GroceryItem);
            _isEditing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
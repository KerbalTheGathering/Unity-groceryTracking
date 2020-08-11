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

namespace rtome.Scripts.Controllers.CellControllers
{
    public class PantryItemPrefabController : EnhancedScrollerCellView, INotifyPropertyChanged
    {
        private PantryInventoryViewController _viewController;
        [ShowInInspector, EnableGUI] private PantryItem _pantryItem;
        [ShowInInspector, EnableGUI] private bool _isEditing = false;
        public PantryItemViewController viewConfiguration;
        public PantryItemEditController editConfiguration;

        public PantryItem PantryItem
        {
            get => _pantryItem;
            set
            {
                if (_pantryItem == value)
                    return;
                _pantryItem = value;
                NotifyPropertyChanged(nameof(PantryItem));
            }
        }

        public EventHandler<PantryItemAddedEventArgs> NotifyEditPantryItem { get; set; }
        public EventHandler<PantryItemAddedEventArgs> NotifyDeletePantryItem { get; set; }
        public EventHandler<PantryItemAddedEventArgs> NotifySavePantryItem { get; set; }

        public void SetData(PantryInventoryViewController viewController, PantryItem item)
        {
            _viewController = viewController;
            PantryItem = item;
            // itemNameText.text = PantryItem.itemName;
            // itemQuantityText.text = PantryItem.quantity.ToString("F2");

            if (_isEditing)
            {
                editConfiguration.Configure(this, PantryItem);
                ConfigureForEdit();
            }
            else
            {
                viewConfiguration.Configure(this, PantryItem);
                ConfigureForView();
            }
        }

        public void OnEditButtonClicked()
        {
            editConfiguration.Configure(this, PantryItem);
            ConfigureForEdit();

            // itemEditButton.gameObject.SetActive(false);
            // itemDeleteButton.gameObject.SetActive(true);
            // NotifyEditPantryItem?.Invoke(this, new PantryItemAddedEventArgs(_pantryItem));
        }

        public void OnSaveButtonClicked()
        {
            viewConfiguration.viewController = this;
            _viewController.mainController.pantryInventory.data[PantryItem.id].itemName =
                editConfiguration.nameText.text;
            _viewController.mainController.pantryInventory.data[PantryItem.id].quantity =
                float.Parse(editConfiguration.quantityText.text);
            WebDataAccessor.UpdatePantryInventoryItem(_viewController.mainController.pantryInventory.data[PantryItem.id]);
            ConfigureForView();
        }

        public void OnDeleteButtonClicked()
        {
            WebDataAccessor.DeletePantryInventoryItem(PantryItem);
            _viewController.mainController.pantryInventory.Remove(PantryItem);
            _viewController.ReloadData();
        }

        private void Start()
        {
            editConfiguration.saveButton.onClick.AddListener(OnSaveButtonClicked);
            PropertyChanged += OnPropertyChanged;
        }

        private void OnDestroy()
        {
            PropertyChanged -= OnPropertyChanged;
        }

        private void ConfigureForEdit()
        {
            editConfiguration.gameObject.SetActive(true);
            viewConfiguration.gameObject.SetActive(false);
            editConfiguration.Configure(this, PantryItem);
            _isEditing = true;
        }

        private void ConfigureForView()
        {
            editConfiguration.gameObject.SetActive(false);
            viewConfiguration.gameObject.SetActive(true);
            viewConfiguration.Configure(this, PantryItem);
            _isEditing = false;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
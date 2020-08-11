using DefaultNamespace;
using EnhancedUI.EnhancedScroller;
using rtome.Scripts.Controllers.CellControllers;
using rtome.Scripts.Controllers.Footer;
using rtome.Scripts.Controllers.FormControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace rtome.Scripts.Controllers.Body
{
    public class PantryInventoryViewController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public string headerTitle;
        [Title("List Components:")]
        public EnhancedScroller scroller;
        public PantryItemPrefabController itemPrefabController;
        public AddNewPantryFormController addNewFormController;

        [Title("Configuration:")]
        public MainServiceController mainController;
        public FooterController footerController;

        private void OnEnable()
        {
            mainController.SetHeaderText(headerTitle);
        }

        private void Start()
        {
            addNewFormController.NotifyItemAdded += OnItemAdded; 
            scroller.Delegate = this;
            scroller.ReloadData();
        }

        private void OnDestroy()
        {
            addNewFormController.NotifyItemAdded -= OnItemAdded;
        }

        private void OnPantryItemEditNotify(object sender, PantryItemAddedEventArgs args)
        {
            addNewFormController.itemNameInputField.text = args.PantryItem.itemName;
            addNewFormController.itemQuantityInputField.text = args.PantryItem.quantity.ToString("F2");
            mainController.pantryInventory.Remove(args.PantryItem);
        }

        private void OnPantryItemDeleteNotify(object sender, PantryItemAddedEventArgs args)
        {
            addNewFormController.itemNameInputField.text = string.Empty;
            addNewFormController.itemQuantityInputField.text = string.Empty;
            ReloadData();
        }

        private void OnPantryItemSaveNotify(object sender, PantryItemAddedEventArgs args)
        {
            
        }

        private void OnItemAdded(object sender, PantryItemAddedEventArgs args)
        {
            ReloadData();
        }

        public void ReloadData()
        {
            mainController.LoadPantryItemsFromWeb();
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return mainController.pantryInventory.data == null ? 0 : mainController.pantryInventory.Count();
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 350f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = Instantiate(itemPrefabController) as PantryItemPrefabController;
            cellView.SetData( this,
                mainController
                    .pantryInventory
                    .GetAtIndex(dataIndex)
                );
            return cellView;
        }
    }
}
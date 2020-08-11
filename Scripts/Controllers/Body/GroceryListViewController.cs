using DefaultNamespace;
using EnhancedUI.EnhancedScroller;
using rtome.Scripts.Controllers.CellControllers;
using rtome.Scripts.Controllers.FormControllers;
using rtome.Scripts.Controllers.Header;
using Sirenix.OdinInspector;
using UnityEngine;

namespace rtome.Scripts.Controllers.Body
{
    public class GroceryListViewController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public string headerTitle;
        [Title("List Components:")] 
        public EnhancedScroller scroller;
        public GroceryItemPrefabController itemPrefabController;
        public AddNewGroceryFormController addNewFormController;
        public GroceryTrackerHeaderController headerController;
        [Title("Configuration:")] 
        public MainServiceController mainController;

        private void OnEnable()
        {
            mainController.SetHeaderText(headerTitle);
        }

        private void Start()
        {
            addNewFormController.NotifyItemAdded += OnItemAdded;
            scroller.Delegate = this;
            scroller.ReloadData();
            headerController.SetText();
        }

        private void OnDestroy()
        {
            addNewFormController.NotifyItemAdded -= OnItemAdded;
        }

        public void ReloadData()
        {
            mainController.LoadGroceryListFromWeb();
            scroller.ReloadData();
            headerController.SetText();
        }

        private void OnItemAdded(object sender, GroceryItemAddedEventArgs args)
        {
            mainController.LoadGroceryListFromWeb();
            ReloadData();
            headerController.SetText();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return mainController.groceryListInventory == null ? 0 : mainController.groceryListInventory.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 350f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = Instantiate(itemPrefabController) as GroceryItemPrefabController;
            cellView.SetData(this,
                mainController.groceryListInventory[dataIndex]
                );
            return cellView;
        }
    }
}
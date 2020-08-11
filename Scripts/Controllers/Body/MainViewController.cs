using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.Body
{
    public class MainViewController : MonoBehaviour
    {
        public string headerTitle;
        [Title("Main Page Buttons: ")]
        public Button pantryButton;
        public Button groceryButton;
        public Button outOfStockButton;

        [Title("Configuration: ")] 
        public MainServiceController mainController;

        private void Awake()
        {
            mainController.SetHeaderText(headerTitle);
        }
        
        private void Start()
        {
            pantryButton.onClick.AddListener(OnPantryButtonClicked);
            groceryButton.onClick.AddListener(OnGroceryButtonClicked);
            outOfStockButton.onClick.AddListener(OnOutOfStockButtonClicked);
        }

        private void OnDestroy()
        {
            pantryButton.onClick.RemoveAllListeners();
            groceryButton.onClick.RemoveAllListeners();
            outOfStockButton.onClick.RemoveAllListeners();
        }

        private void OnPantryButtonClicked()
        {
            mainController.pantryInventoryViewController.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnGroceryButtonClicked()
        {
            mainController.groceryListViewController.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnOutOfStockButtonClicked()
        {
            
        }
    }
}
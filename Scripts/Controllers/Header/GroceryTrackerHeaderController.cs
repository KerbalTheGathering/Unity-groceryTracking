using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace rtome.Scripts.Controllers.Header
{
    public class GroceryTrackerHeaderController : MonoBehaviour
    {
        [Title("Settings:")] 
        public string stringComponent;

        [Title("Configuration:")] 
        public MainServiceController mainController;
        public TMP_Text totalReadout;

        private void OnEnable()
        {
            SetText();
        }

        public void SetText()
        {
            if (mainController.groceryListInventory.Count == 0)
            {
                totalReadout.text
                    = mainController.groceryListInventory.Count
                      + stringComponent
                      + "0.00";
            }
            else
            {
                int items = mainController.groceryListInventory.Count;
                float cost = 0.00f;
                foreach (var item in mainController.groceryListInventory)
                {
                    cost += float.Parse(item.itemPrice) 
                            * float.Parse(item.itemCount);
                }

                totalReadout.text
                    = items.ToString("F0")
                      + stringComponent
                      + cost.ToString("F2");
            }
        }
    }
}
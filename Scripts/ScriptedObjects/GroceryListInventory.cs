using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace rtome.Scripts.ScriptedObjects
{
    [CreateAssetMenu(fileName = "GroceryListInventory", menuName = "Inventory/GroceryList", order = 1)]
    public class GroceryListInventory : ScriptableObject
    {
        [ShowInInspector, EnableGUI]
        public Dictionary<string, GroceryItem> data;
    }
}
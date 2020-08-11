using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PantryItem
    {
        public string id;
        public string itemName;
        public float quantity;

        public PantryItem()
        {
            id = Guid.NewGuid().ToString();
            itemName = string.Empty;
            quantity = 0f;
        }

        public PantryItem(string itemName, float quantity)
        {
            id = Guid.NewGuid().ToString();
            this.itemName = itemName;
            this.quantity = quantity;
        }
        
        public PantryItem(string id, string itemName, float quantity)
        {
            this.id = id;
            this.itemName = itemName;
            this.quantity = quantity;
        }
    }
}
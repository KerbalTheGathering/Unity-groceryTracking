using System;
using System.Collections.Generic;
using Ludiq.PeekCore;
using UnityEngine;

namespace DefaultNamespace
{
    public class GroceryItem
    {
        public string id;
        public string itemName;
        public string itemCount;
        public string itemPrice;

        public GroceryItem()
        {
            id = Guid.NewGuid().ToString();
            itemName = string.Empty;
        }

        public GroceryItem(string itemName, string itemCount, string itemPrice)
        {
            id = Guid.NewGuid().ToString();
            this.itemName = itemName;
            this.itemCount = itemCount;
            this.itemPrice = itemPrice;
        }

        public GroceryItem(string id, string itemName, string itemCount, string itemPrice)
        {
            this.id = id;
            this.itemName = itemName;
            this.itemCount = itemCount;
            this.itemPrice = itemPrice;
        }
    }

    public class GroceryItemWrapper
    {
        public GroceryItem[] groceryItems;
    }
}
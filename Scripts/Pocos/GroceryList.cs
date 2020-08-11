using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace DefaultNamespace
{
    public class GroceryList
    {
        public string id;
        public string name;
        public string itmCount;
        public string[] items;

        public GroceryList()
        {
            id = Guid.NewGuid().ToString();
            name = string.Empty;
            itmCount = "0";
            items = new string[0];
        }

        public GroceryList(string name, string itmCount, string[] items)
        {
            id = Guid.NewGuid().ToString();
            this.name = name;
            this.itmCount = itmCount;
            this.items = items;
        }
        
        public GroceryList(string id, string name, string itmCount, string[] items)
        {
            this.id = id;
            this.name = name;
            this.itmCount = itmCount;
            this.items = items;
        }
    }
}
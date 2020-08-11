using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace rtome.Scripts.ScriptedObjects
{
    [CreateAssetMenu(fileName = "PantryInventory", menuName = "Inventory/PantryInventory", order = 0)]
    public class PantryInventory : SerializedScriptableObject
    {
        [ShowInInspector, EnableGUI]
        public Dictionary<string, PantryItem> data;

        public int Count()
        { 
            return data.Values.Count;
        } 

        public void Add(PantryItem item)
        {
            data.Add(item.id, item);
        }

        public void Remove(PantryItem item)
        {
            if (data.ContainsKey(item.id))
                data.Remove(item.id);
        }

        public PantryItem GetAtIndex(int index)
        {
            return data.Values.ElementAt(index);
        }

        public PantryItem GetByKey(string key)
        {
            if (data.ContainsKey(key))
                return data[key];
            return null;
        }

        public void OverwriteAtKey(string key, PantryItem item)
        {
            var val = GetByKey(key);
            if (val != null)
            {
                Remove(GetByKey(key));
                Add(item);
            }
        }
    }
}
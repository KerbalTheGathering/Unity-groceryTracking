using System;

namespace DefaultNamespace
{
    public class OutOfStockItem
    {
        public readonly int id;
        public readonly string itemName;
        public readonly Locations location;

        public OutOfStockItem()
        {
            itemName = string.Empty;
            location = Locations.None;
            id = GetHashCode();
        }

        public OutOfStockItem(string itemName, Locations location)
        {
            this.itemName = itemName;
            this.location = Locations.None;
            this.id = GetHashCode();
        }

        public override int GetHashCode()
        {
            return new Tuple<string, string>(itemName, location.ToString()).GetHashCode();
        }
    }
}
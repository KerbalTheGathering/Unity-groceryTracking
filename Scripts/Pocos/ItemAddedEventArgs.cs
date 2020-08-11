namespace DefaultNamespace
{
    public class ItemAddedEventArgs
    {
        public GroceryItem GroceryItem { get; set; }

        public ItemAddedEventArgs(GroceryItem groceryItem)
        {
            GroceryItem = groceryItem;
        }
    }
}
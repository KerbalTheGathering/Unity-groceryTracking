namespace DefaultNamespace
{
    public class GroceryItemAddedEventArgs
    {
        public GroceryItem GroceryItem { get; set; }

        public GroceryItemAddedEventArgs(GroceryItem groceryItem)
        {
            GroceryItem = groceryItem;
        }
    }
}
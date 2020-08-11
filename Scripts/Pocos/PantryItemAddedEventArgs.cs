namespace DefaultNamespace
{
    public class PantryItemAddedEventArgs
    {
        public int OriginalId { get; set; }
        public PantryItem PantryItem { get; set; }

        public PantryItemAddedEventArgs(PantryItem pantryItem)
        {
            PantryItem = pantryItem;
        }
}
}
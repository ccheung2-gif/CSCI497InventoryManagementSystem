namespace InventoryDataAccess.Models
{
    // category for the dropdowns
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // what the dropdown shows
        public override string ToString()
        {
            return Name;
        }
    }
}

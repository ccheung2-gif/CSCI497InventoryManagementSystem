namespace InventoryDataAccess.Models
{
    // supplier for the dropdown
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }

        // what the dropdown shows
        public override string ToString()
        {
            return Name;
        }
    }
}

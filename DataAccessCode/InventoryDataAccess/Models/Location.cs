namespace InventoryDataAccess.Models
{
    // location for the dropdown
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }

        // what the dropdown shows
        public override string ToString()
        {
            return Name;
        }
    }
}

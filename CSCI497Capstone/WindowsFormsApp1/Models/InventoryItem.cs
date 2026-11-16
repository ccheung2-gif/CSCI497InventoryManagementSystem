namespace WindowsFormsApp1.Models
{
    /// <summary>
    /// One inventory record. This is the shape the UI works with; the data layer
    /// is responsible for converting database rows to and from this class.
    /// </summary>
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string Location { get; set; }

        /// <summary>True when stock is at or below the reorder level.</summary>
        public bool IsLowStock
        {
            get { return Quantity <= ReorderLevel; }
        }

        public InventoryItem Clone()
        {
            return (InventoryItem)MemberwiseClone();
        }
    }
}

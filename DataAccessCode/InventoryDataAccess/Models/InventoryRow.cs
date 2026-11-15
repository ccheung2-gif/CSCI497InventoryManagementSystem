namespace InventoryDataAccess.Models
{
    // one row of the form1 grid
    public class InventoryRow
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string LocationName { get; set; }
        public bool IsLowStock { get; set; }
    }
}

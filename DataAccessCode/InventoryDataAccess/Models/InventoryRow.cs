namespace InventoryDataAccess.Models
{
    // one form1 grid row = one product at one location
    public class InventoryRow
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string LocationName { get; set; }
        public bool IsLowStock { get; set; }
    }
}

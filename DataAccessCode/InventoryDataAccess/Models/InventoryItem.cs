namespace InventoryDataAccess.Models
{
    // one item on form2 add/edit. ids are 0 if new/not set
    public class InventoryItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

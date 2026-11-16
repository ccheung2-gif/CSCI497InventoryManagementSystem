using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Temporary stand-in for the database so the UI can be built and tested
    /// now. Data lives only while the app is running.
    /// </summary>
    public class InMemoryInventoryData : IInventoryData
    {
        private readonly List<InventoryItem> _items = new List<InventoryItem>();
        private int _nextId = 1;

        private static readonly List<string> Categories = new List<string>
        {
            "Electronics", "Office Supplies", "Furniture", "Tools"
        };

        private static readonly List<string> Suppliers = new List<string>
        {
            "Acme Corp", "Global Supply Co", "Northwind Traders"
        };

        public InMemoryInventoryData()
        {
            Seed("USB Cable", "Electronics", "3 ft USB-C cable", "Acme Corp", 40, 10, 5.99m, "Shelf A1");
            Seed("Wireless Mouse", "Electronics", "2.4 GHz mouse", "Northwind Traders", 6, 8, 19.99m, "Shelf A2");
            Seed("Printer Paper", "Office Supplies", "Letter, 500 sheets", "Global Supply Co", 25, 15, 8.49m, "Shelf B1");
            Seed("Stapler", "Office Supplies", "Standard desktop stapler", "Global Supply Co", 3, 5, 12.00m, "Shelf B2");
            Seed("Office Chair", "Furniture", "Ergonomic mesh chair", "Northwind Traders", 4, 2, 149.00m, "Warehouse");
            Seed("Screwdriver Set", "Tools", "12-piece set", "Acme Corp", 12, 4, 24.50m, "Shelf C1");
        }

        public List<InventoryItem> GetAllItems()
        {
            // Return copies so the UI can't change stored data without calling UpdateItem.
            return _items.Select(i => i.Clone()).ToList();
        }

        public int AddItem(InventoryItem item)
        {
            InventoryItem copy = item.Clone();
            copy.Id = _nextId++;
            _items.Add(copy);
            return copy.Id;
        }

        public void UpdateItem(InventoryItem item)
        {
            int index = _items.FindIndex(i => i.Id == item.Id);
            if (index < 0)
            {
                throw new InvalidOperationException("Item " + item.Id + " no longer exists.");
            }

            _items[index] = item.Clone();
        }

        public void DeleteItem(int id)
        {
            _items.RemoveAll(i => i.Id == id);
        }

        public List<string> GetCategories()
        {
            return new List<string>(Categories);
        }

        public List<string> GetSuppliers()
        {
            return new List<string>(Suppliers);
        }

        private void Seed(string name, string category, string description, string supplier,
                          int quantity, int reorderLevel, decimal unitPrice, string location)
        {
            AddItem(new InventoryItem
            {
                Name = name,
                Category = category,
                Description = description,
                Supplier = supplier,
                Quantity = quantity,
                ReorderLevel = reorderLevel,
                UnitPrice = unitPrice,
                Location = location
            });
        }
    }
}

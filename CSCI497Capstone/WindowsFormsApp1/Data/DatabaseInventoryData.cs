using System;
using System.Collections.Generic;
using System.Linq;
using InventoryDataAccess;
using UiItem = WindowsFormsApp1.Models.InventoryItem;
using DbItem = InventoryDataAccess.Models.InventoryItem;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Connects the UI to the InventoryDataAccess project (Jeffrey's SQLite code).
    ///
    /// The UI works with names (category "Tools", supplier "Gadget Source"),
    /// while the database stores IDs. This class converts between the two,
    /// so neither the forms nor the data access code need to know about the other.
    /// Exceptions are left to propagate; the forms show them to the user.
    /// </summary>
    public class DatabaseInventoryData : IInventoryData
    {
        public List<UiItem> GetAllItems()
        {
            // The forms do search/category/low-stock filtering in memory,
            // so ask for everything.
            var rows = InventoryData.GetInventory("", 0, false);
            var suppliers = LookupData.GetSuppliers().ToDictionary(s => s.SupplierId, s => s.Name);

            var items = new List<UiItem>();
            foreach (var row in rows)
            {
                // GetInventory doesn't return Description or Supplier, which the
                // edit screen and search need, so load the full record as well.
                DbItem full = InventoryData.GetItem(row.ProductId);
                string supplier;
                suppliers.TryGetValue(full != null ? full.SupplierId : 0, out supplier);

                items.Add(new UiItem
                {
                    Id = row.ProductId,
                    Name = row.ProductName,
                    Category = row.CategoryName,
                    Description = full != null ? full.Description : "",
                    Supplier = supplier ?? "",
                    Quantity = row.Quantity,
                    ReorderLevel = row.ReorderLevel,
                    UnitPrice = row.UnitPrice,
                    Location = row.LocationName
                });
            }

            return items;
        }

        public int AddItem(UiItem item)
        {
            return ItemData.AddItem(ToDbItem(item));
        }

        public void UpdateItem(UiItem item)
        {
            ItemData.UpdateItem(ToDbItem(item));
        }

        public void DeleteItem(int id)
        {
            ItemData.DeleteItem(id);
        }

        public List<string> GetCategories()
        {
            return LookupData.GetCategories().Select(c => c.Name).ToList();
        }

        public List<string> GetSuppliers()
        {
            return LookupData.GetSuppliers().Select(s => s.Name).ToList();
        }

        /// <summary>Converts the UI item (names) into the data-access item (IDs).</summary>
        private static DbItem ToDbItem(UiItem item)
        {
            return new DbItem
            {
                ProductId = item.Id,
                Name = item.Name,
                Description = item.Description,
                CategoryId = FindId(item.Category, LookupData.GetCategories(), c => c.Name, c => c.CategoryId, "category"),
                SupplierId = FindId(item.Supplier, LookupData.GetSuppliers(), s => s.Name, s => s.SupplierId, "supplier"),
                LocationId = FindId(item.Location, LookupData.GetLocations(), l => l.Name, l => l.LocationId, "location"),
                Quantity = item.Quantity,
                ReorderLevel = item.ReorderLevel,
                UnitPrice = item.UnitPrice
            };
        }

        /// <summary>
        /// Looks up the ID for a name. Blank means "nothing picked" (0, stored as NULL).
        /// Names are UNIQUE in the schema, so a match is unambiguous.
        /// </summary>
        private static int FindId<T>(string name, IEnumerable<T> list, Func<T, string> getName,
                                     Func<T, int> getId, string kind)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return 0;
            }

            foreach (T entry in list)
            {
                if (string.Equals(getName(entry), name, StringComparison.OrdinalIgnoreCase))
                {
                    return getId(entry);
                }
            }

            throw new InvalidOperationException("The " + kind + " \"" + name + "\" no longer exists in the database.");
        }
    }
}

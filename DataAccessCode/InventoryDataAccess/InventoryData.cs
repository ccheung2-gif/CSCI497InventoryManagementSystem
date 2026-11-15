using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using InventoryDataAccess.Models;

namespace InventoryDataAccess
{
    // inventory reads for form1 + form2
    public static class InventoryData
    {
        // rows for the form1 grid w/ filters
        public static List<InventoryRow> GetInventory(string searchText, int categoryId, bool lowStockOnly)
        {
            List<InventoryRow> rows = new List<InventoryRow>();

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                // product + category and location names, then search / category / low stock filters
                command.CommandText = @"
                    SELECT p.PRODUCT_ID, p.PRODUCT_NAME, c.CATEGORY_NAME, p.QUANTITY,
                           p.REORDER_LEVEL, p.PRICE, l.LOCATION_NAME
                    FROM PRODUCT p
                    LEFT JOIN CATEGORY c           ON c.CATEGORY_ID = p.CATEGORY_ID
                    LEFT JOIN INVENTORY_LOCATION l ON l.LOCATION_ID = p.LOCATION_ID
                    WHERE p.PRODUCT_NAME LIKE $search
                      AND ($categoryId = 0 OR p.CATEGORY_ID = $categoryId)
                      AND ($lowStockOnly = 0 OR p.QUANTITY <= p.REORDER_LEVEL)
                    ORDER BY p.PRODUCT_NAME;";

                // params so nobody can mess w/ the sql
                command.Parameters.AddWithValue("$search", "%" + searchText + "%");
                command.Parameters.AddWithValue("$categoryId", categoryId);
                command.Parameters.AddWithValue("$lowStockOnly", lowStockOnly);

                // each row -> InventoryRow. empty category/location -> ""
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InventoryRow row = new InventoryRow();
                        row.ProductId = reader.GetInt32(0);
                        row.ProductName = reader.GetString(1);
                        if (reader.IsDBNull(2)) { row.CategoryName = ""; } else { row.CategoryName = reader.GetString(2); }
                        row.Quantity = reader.GetInt32(3);
                        row.ReorderLevel = reader.GetInt32(4);
                        row.UnitPrice = reader.GetDecimal(5);
                        if (reader.IsDBNull(6)) { row.LocationName = ""; } else { row.LocationName = reader.GetString(6); }
                        row.IsLowStock = row.Quantity <= row.ReorderLevel;
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        // one item for the edit screen (null if not there)
        public static InventoryItem GetItem(int productId)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT PRODUCT_ID, PRODUCT_NAME, DESCRIPTION, CATEGORY_ID, SUPPLIER_ID,
                           LOCATION_ID, QUANTITY, REORDER_LEVEL, PRICE
                    FROM PRODUCT
                    WHERE PRODUCT_ID = $productId;";
                command.Parameters.AddWithValue("$productId", productId);

                // into an InventoryItem, 0 = nothing picked, "" = no description
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    InventoryItem item = new InventoryItem();
                    item.ProductId = reader.GetInt32(0);
                    item.Name = reader.GetString(1);
                    if (reader.IsDBNull(2)) { item.Description = ""; } else { item.Description = reader.GetString(2); }
                    if (reader.IsDBNull(3)) { item.CategoryId = 0; } else { item.CategoryId = reader.GetInt32(3); }
                    if (reader.IsDBNull(4)) { item.SupplierId = 0; } else { item.SupplierId = reader.GetInt32(4); }
                    if (reader.IsDBNull(5)) { item.LocationId = 0; } else { item.LocationId = reader.GetInt32(5); }
                    item.Quantity = reader.GetInt32(6);
                    item.ReorderLevel = reader.GetInt32(7);
                    item.UnitPrice = reader.GetDecimal(8);
                    return item;
                }
            }
        }
    }
}

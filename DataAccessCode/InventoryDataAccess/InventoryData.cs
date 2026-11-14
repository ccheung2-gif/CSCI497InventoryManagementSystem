using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using InventoryDataAccess.Models;

namespace InventoryDataAccess
{
    // inventory reads for form1 + form2
    public static class InventoryDataClass
    {
        // rows for the form1 grid w/ filters
        public static List<InventoryRow> GetInventory(string searchText, int categoryId, bool lowStockOnly)
        {
            List<InventoryRow> rows = new List<InventoryRow>();

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                // join everything, then search / category / low stock filters
                command.CommandText = @"
                    SELECT s.STOCK_ID, p.PRODUCT_ID, p.SKU, p.PRODUCT_NAME, c.CATEGORY_NAME,
                           s.QUANTITY, s.REORDER_LEVEL, p.PRICE, l.LOCATION_NAME
                    FROM STOCK_LEVEL s
                    JOIN PRODUCT p            ON p.PRODUCT_ID  = s.PRODUCT_ID
                    JOIN INVENTORY_LOCATION l ON l.LOCATION_ID = s.LOCATION_ID
                    LEFT JOIN CATEGORY c      ON c.CATEGORY_ID = p.CATEGORY_ID
                    WHERE (p.PRODUCT_NAME LIKE $search OR p.SKU LIKE $search)
                      AND ($categoryId = 0 OR p.CATEGORY_ID = $categoryId)
                      AND ($lowStockOnly = 0 OR s.QUANTITY <= s.REORDER_LEVEL)
                    ORDER BY p.PRODUCT_NAME, l.LOCATION_NAME;";

                // params so nobody can mess w/ the sql
                command.Parameters.AddWithValue("$search", "%" + searchText + "%");
                command.Parameters.AddWithValue("$categoryId", categoryId);
                command.Parameters.AddWithValue("$lowStockOnly", lowStockOnly);

                // each row -> InventoryRow
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InventoryRow row = new InventoryRow();
                        row.StockId = reader.GetInt32(0);
                        row.ProductId = reader.GetInt32(1);
                        row.Sku = reader.GetString(2);
                        row.ProductName = reader.GetString(3);
                        if (reader.IsDBNull(4)) { row.CategoryName = ""; } else { row.CategoryName = reader.GetString(4); }
                        row.Quantity = reader.GetInt32(5);
                        row.ReorderLevel = reader.GetInt32(6);
                        row.UnitPrice = reader.GetDecimal(7);
                        row.LocationName = reader.GetString(8);
                        row.IsLowStock = row.Quantity <= row.ReorderLevel;
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        // one item for the edit screen (null if not there)
        public static InventoryItem GetItem(int stockId)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                // product + supplier + stock row
                command.CommandText = @"
                    SELECT p.PRODUCT_ID, s.STOCK_ID, p.SKU, p.PRODUCT_NAME, p.CATEGORY_ID,
                           p.SUPPLIER_ID, s.LOCATION_ID, s.QUANTITY, s.REORDER_LEVEL, p.PRICE
                    FROM STOCK_LEVEL s
                    JOIN PRODUCT p ON p.PRODUCT_ID = s.PRODUCT_ID
                    WHERE s.STOCK_ID = $stockId;";
                command.Parameters.AddWithValue("$stockId", stockId);

                // into an InventoryItem, 0 = no category/supplier
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    InventoryItem item = new InventoryItem();
                    item.ProductId = reader.GetInt32(0);
                    item.StockId = reader.GetInt32(1);
                    item.Sku = reader.GetString(2);
                    item.Name = reader.GetString(3);
                    if (reader.IsDBNull(4)) { item.CategoryId = 0; } else { item.CategoryId = reader.GetInt32(4); }
                    if (reader.IsDBNull(5)) { item.SupplierId = 0; } else { item.SupplierId = reader.GetInt32(5); }
                    item.LocationId = reader.GetInt32(6);
                    item.Quantity = reader.GetInt32(7);
                    item.ReorderLevel = reader.GetInt32(8);
                    item.UnitPrice = reader.GetDecimal(9);
                    return item;
                }
            }
        }
    }
}

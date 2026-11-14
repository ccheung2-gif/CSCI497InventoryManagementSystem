using System;
using Microsoft.Data.Sqlite;
using InventoryDataAccess.Models;

namespace InventoryDataAccess
{
    // add/update/delete items. all or nothing
    public static class ItemData
    {
        // new product + starting stock, returns new id
        public static int AddItem(InventoryItem item, int userId)
        {
            CheckItem(item);
            if (SkuExists(item.Sku, 0))
            {
                throw new Exception("The SKU " + item.Sku + " is already used by another product.");
            }

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                int productId;

                // insert product, grab new id
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"
                        INSERT INTO PRODUCT (SKU, PRODUCT_NAME, PRICE, CATEGORY_ID, SUPPLIER_ID)
                        VALUES ($sku, $name, $price, $categoryId, $supplierId);
                        SELECT last_insert_rowid();";
                    command.Parameters.AddWithValue("$sku", item.Sku);
                    command.Parameters.AddWithValue("$name", item.Name);
                    command.Parameters.AddWithValue("$price", item.UnitPrice);
                    command.Parameters.AddWithValue("$categoryId", IdOrNull(item.CategoryId));
                    command.Parameters.AddWithValue("$supplierId", IdOrNull(item.SupplierId));
                    productId = Convert.ToInt32(command.ExecuteScalar());
                }

                // stock row
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"
                        INSERT INTO STOCK_LEVEL (PRODUCT_ID, LOCATION_ID, REORDER_LEVEL, QUANTITY)
                        VALUES ($productId, $locationId, $reorderLevel, $quantity);";
                    command.Parameters.AddWithValue("$productId", productId);
                    command.Parameters.AddWithValue("$locationId", item.LocationId);
                    command.Parameters.AddWithValue("$reorderLevel", item.ReorderLevel);
                    command.Parameters.AddWithValue("$quantity", item.Quantity);
                    command.ExecuteNonQuery();
                }

                // starting stock counts as a Receipt
                if (item.Quantity > 0)
                {
                    StockData.AddMovement(connection, transaction, productId, item.LocationId, userId, StockData.Receipt, item.Quantity);
                }

                transaction.Commit();
                return productId;
            }
        }

        // save edits, qty change gets logged as Adjustment
        public static void UpdateItem(InventoryItem item, int userId)
        {
            CheckItem(item);
            if (SkuExists(item.Sku, item.ProductId))
            {
                throw new Exception("The SKU " + item.Sku + " is already used by another product.");
            }

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                // product
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"
                        UPDATE PRODUCT
                        SET SKU = $sku, PRODUCT_NAME = $name, PRICE = $price,
                            CATEGORY_ID = $categoryId, SUPPLIER_ID = $supplierId
                        WHERE PRODUCT_ID = $productId;";
                    command.Parameters.AddWithValue("$sku", item.Sku);
                    command.Parameters.AddWithValue("$name", item.Name);
                    command.Parameters.AddWithValue("$price", item.UnitPrice);
                    command.Parameters.AddWithValue("$categoryId", IdOrNull(item.CategoryId));
                    command.Parameters.AddWithValue("$supplierId", IdOrNull(item.SupplierId));
                    command.Parameters.AddWithValue("$productId", item.ProductId);
                    command.ExecuteNonQuery();
                }

                // get old qty so we know the difference
                int oldQuantity;
                int locationId;
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = "SELECT QUANTITY, LOCATION_ID FROM STOCK_LEVEL WHERE STOCK_ID = $stockId;";
                    command.Parameters.AddWithValue("$stockId", item.StockId);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("That stock record was not found.");
                        }
                        oldQuantity = reader.GetInt32(0);
                        locationId = reader.GetInt32(1);
                    }
                }

                // stock row
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = "UPDATE STOCK_LEVEL SET QUANTITY = $quantity, REORDER_LEVEL = $reorderLevel WHERE STOCK_ID = $stockId;";
                    command.Parameters.AddWithValue("$quantity", item.Quantity);
                    command.Parameters.AddWithValue("$reorderLevel", item.ReorderLevel);
                    command.Parameters.AddWithValue("$stockId", item.StockId);
                    command.ExecuteNonQuery();
                }

                // log it if qty changed
                int difference = item.Quantity - oldQuantity;
                if (difference != 0)
                {
                    StockData.AddMovement(connection, transaction, item.ProductId, locationId, userId, StockData.Adjustment, difference);
                }

                transaction.Commit();
            }
        }

        // delete product + everything pointing at it
        public static void DeleteItem(int productId)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                // kids first then product or FKs complain
                string[] deleteStatements =
                {
                    "DELETE FROM INVENTORY_MOVEMENT WHERE PRODUCT_ID = $productId;",
                    "DELETE FROM STOCK_LEVEL WHERE PRODUCT_ID = $productId;",
                    "DELETE FROM PRODUCT WHERE PRODUCT_ID = $productId;"
                };
                foreach (string sql in deleteStatements)
                {
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("$productId", productId);
                        command.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
        }

        // is the sku taken by some other product
        private static bool SkuExists(string sku, int exceptProductId)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM PRODUCT WHERE SKU = $sku AND PRODUCT_ID <> $productId;";
                command.Parameters.AddWithValue("$sku", sku);
                command.Parameters.AddWithValue("$productId", exceptProductId);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        // basic checks before saving
        private static void CheckItem(InventoryItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Sku)) throw new Exception("SKU is required.");
            if (string.IsNullOrWhiteSpace(item.Name)) throw new Exception("Name is required.");
            if (item.LocationId == 0) throw new Exception("Location is required.");
            if (item.Quantity < 0) throw new Exception("Quantity cannot be negative.");
            if (item.ReorderLevel < 0) throw new Exception("Reorder level cannot be negative.");
            if (item.UnitPrice < 0) throw new Exception("Unit price cannot be negative.");
        }

        // 0 = nothing picked -> NULL in db (category, supplier)
        private static object IdOrNull(int id)
        {
            if (id == 0)
            {
                return DBNull.Value;
            }
            return id;
        }
    }
}

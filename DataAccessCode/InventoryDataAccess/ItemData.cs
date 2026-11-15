using System;
using Microsoft.Data.Sqlite;
using InventoryDataAccess.Models;

namespace InventoryDataAccess
{
    // add/update/delete items
    public static class ItemData
    {
        // new item, returns new id
        public static int AddItem(InventoryItem item)
        {
            CheckItem(item);

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO PRODUCT (PRODUCT_NAME, DESCRIPTION, CATEGORY_ID, SUPPLIER_ID, LOCATION_ID, QUANTITY, REORDER_LEVEL, PRICE)
                    VALUES ($name, $description, $categoryId, $supplierId, $locationId, $quantity, $reorderLevel, $price);
                    SELECT last_insert_rowid();";
                AddValues(command, item);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        // save edits
        public static void UpdateItem(InventoryItem item)
        {
            CheckItem(item);

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE PRODUCT
                    SET PRODUCT_NAME = $name, DESCRIPTION = $description, CATEGORY_ID = $categoryId,
                        SUPPLIER_ID = $supplierId, LOCATION_ID = $locationId, QUANTITY = $quantity,
                        REORDER_LEVEL = $reorderLevel, PRICE = $price
                    WHERE PRODUCT_ID = $productId;";
                AddValues(command, item);
                command.Parameters.AddWithValue("$productId", item.ProductId);
                command.ExecuteNonQuery();
            }
        }

        // delete one item
        public static void DeleteItem(int productId)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM PRODUCT WHERE PRODUCT_ID = $productId;";
                command.Parameters.AddWithValue("$productId", productId);
                command.ExecuteNonQuery();
            }
        }

        // basic checks before saving
        private static void CheckItem(InventoryItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name)) throw new Exception("Name is required.");
            if (item.Quantity < 0) throw new Exception("Quantity cannot be negative.");
            if (item.ReorderLevel < 0) throw new Exception("Reorder level cannot be negative.");
            if (item.UnitPrice < 0) throw new Exception("Unit price cannot be negative.");
        }

        // fills in the $values shared by add and update. params so nobody can mess w/ the sql
        private static void AddValues(SqliteCommand command, InventoryItem item)
        {
            command.Parameters.AddWithValue("$name", item.Name);
            command.Parameters.AddWithValue("$description", TextOrNull(item.Description));
            command.Parameters.AddWithValue("$categoryId", IdOrNull(item.CategoryId));
            command.Parameters.AddWithValue("$supplierId", IdOrNull(item.SupplierId));
            command.Parameters.AddWithValue("$locationId", IdOrNull(item.LocationId));
            command.Parameters.AddWithValue("$quantity", item.Quantity);
            command.Parameters.AddWithValue("$reorderLevel", item.ReorderLevel);
            command.Parameters.AddWithValue("$price", item.UnitPrice);
        }

        // 0 = nothing picked -> NULL in db
        private static object IdOrNull(int id)
        {
            if (id == 0)
            {
                return DBNull.Value;
            }
            return id;
        }

        // blank description -> NULL in db
        private static object TextOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return DBNull.Value;
            }
            return text;
        }
    }
}

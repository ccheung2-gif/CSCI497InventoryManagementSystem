using System;
using Microsoft.Data.Sqlite;

namespace InventoryDataAccess
{
    // stock movement log stuff
    public static class StockData
    {
        // movement types, has to match the CHECK in schema.sql
        public const string Receipt = "Receipt";
        public const string Adjustment = "Adjustment";

        // adds one movement row, uses whatever transaction is passed in
        public static void AddMovement(SqliteConnection connection, SqliteTransaction transaction,
            int productId, int locationId, int userId, string movementType, int quantity)
        {
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = @"
                    INSERT INTO INVENTORY_MOVEMENT (PRODUCT_ID, LOCATION_ID, USER_ID, MOVEMENT_TYPE, QUANTITY, MOVEMENT_DATE)
                    VALUES ($productId, $locationId, $userId, $movementType, $quantity, $movementDate);";
                command.Parameters.AddWithValue("$productId", productId);
                command.Parameters.AddWithValue("$locationId", locationId);
                command.Parameters.AddWithValue("$userId", userId);
                command.Parameters.AddWithValue("$movementType", movementType);
                command.Parameters.AddWithValue("$quantity", quantity);
                command.Parameters.AddWithValue("$movementDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }
        }
    }
}

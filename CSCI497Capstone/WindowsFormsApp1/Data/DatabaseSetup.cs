using System;
using System.IO;
using InventoryDataAccess;
using Microsoft.Data.Sqlite;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Points the data access code at the database file and builds the file
    /// from schema.sql + seed.sql the first time the app runs. The .db file
    /// isn't in Git, so this saves everyone from building it by hand.
    /// Delete inventory.db next to the .exe to reset to the sample data.
    /// </summary>
    public static class DatabaseSetup
    {
        public static void Initialize()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appFolder, "inventory.db");
            Database.FilePath = dbPath;

            if (File.Exists(dbPath))
            {
                return;
            }

            string scripts = Path.Combine(appFolder, "database");
            string schema = File.ReadAllText(Path.Combine(scripts, "schema.sql"));
            string seed = File.ReadAllText(Path.Combine(scripts, "seed.sql"));

            try
            {
                // ReadWriteCreate makes the file; Database.OpenConnection only opens existing files.
                using (var connection = new SqliteConnection("Data Source=" + dbPath + ";Mode=ReadWriteCreate"))
                {
                    connection.Open();
                    RunScript(connection, schema);
                    RunScript(connection, seed);
                }
            }
            catch
            {
                // Don't leave a half-built database behind.
                SqliteConnection.ClearAllPools();
                File.Delete(dbPath);
                throw;
            }
        }

        private static void RunScript(SqliteConnection connection, string sql)
        {
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}

using Microsoft.Data.Sqlite;

namespace InventoryDataAccess
{
    // opens the sqlite db
    public static class Database
    {
        // where the db file is
        public static string FilePath = "database.db";

        public static SqliteConnection OpenConnection()
        {
            // open existing file, blows up if its not there
            SqliteConnection connection = new SqliteConnection("Data Source=" + FilePath + ";Mode=ReadWrite");
            connection.Open();

            // FK checks are off by default in sqlite, turn em on
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }

            return connection;
        }
    }
}

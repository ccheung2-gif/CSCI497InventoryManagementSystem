using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using InventoryDataAccess.Models;

namespace InventoryDataAccess
{
    // dropdown lists + user id lookup
    public static class LookupData
    {
        // all categories by name
        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT CATEGORY_ID, CATEGORY_NAME FROM CATEGORY ORDER BY CATEGORY_NAME;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryId = reader.GetInt32(0);
                        category.Name = reader.GetString(1);
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        // all suppliers by name
        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER ORDER BY SUPPLIER_NAME;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Supplier supplier = new Supplier();
                        supplier.SupplierId = reader.GetInt32(0);
                        supplier.Name = reader.GetString(1);
                        suppliers.Add(supplier);
                    }
                }
            }

            return suppliers;
        }

        // all locations by name
        public static List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();

            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT LOCATION_ID, LOCATION_NAME FROM INVENTORY_LOCATION ORDER BY LOCATION_NAME;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Location location = new Location();
                        location.LocationId = reader.GetInt32(0);
                        location.Name = reader.GetString(1);
                        locations.Add(location);
                    }
                }
            }

            return locations;
        }

        // user id from username, 0 if no match
        public static int GetUserId(string username)
        {
            using (SqliteConnection connection = Database.OpenConnection())
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT USER_ID FROM USER WHERE USERNAME = $username;";
                command.Parameters.AddWithValue("$username", username);
                object result = command.ExecuteScalar();
                if (result == null)
                {
                    return 0;
                }
                return Convert.ToInt32(result);
            }
        }
    }
}

# How the UI and the database fit together

```
Form1 / Form2  ──►  IInventoryData  ──►  DatabaseInventoryData  ──►  InventoryDataAccess  ──►  SQLite
  (UI, names)        (interface)          (adapter: names ↔ IDs)      (Jeffrey's SQL code)
```

The forms never write SQL. They only call `IInventoryData`
(`CSCI497Capstone/WindowsFormsApp1/Data/IInventoryData.cs`):

| UI action                     | Interface method        | InventoryDataAccess call(s)                         |
|-------------------------------|-------------------------|-----------------------------------------------------|
| Load / Refresh grid           | `GetAllItems()`         | `InventoryData.GetInventory("", 0, false)` + `GetItem(id)` per row |
| Add Item → Save               | `AddItem(item)`         | `ItemData.AddItem`                                  |
| Edit → Save (or double-click) | `UpdateItem(item)`      | `ItemData.UpdateItem`                               |
| Delete                        | `DeleteItem(id)`        | `ItemData.DeleteItem`                               |
| Category drop-downs           | `GetCategories()`       | `LookupData.GetCategories`                          |
| Supplier drop-down            | `GetSuppliers()`        | `LookupData.GetSuppliers`                           |

Search, the category filter and Low Stock are applied in memory on the result of `GetAllItems()`.

## Running it

1. Open `CSCI497Capstone/WindowsFormsApp1.sln` in Visual Studio. It contains both
   `WindowsFormsApp1` and `InventoryDataAccess`. NuGet restores `Microsoft.Data.Sqlite` on build.
2. Press F5. On first run, `DatabaseSetup` creates `inventory.db` next to the .exe by running
   `Inventory_Source/database/schema.sql` then `seed.sql` (both are copied to `bin\...\database\`).
3. To reset to the sample data, delete `bin\Debug\inventory.db` and run again.

The `.db` file is git-ignored, so everyone gets their own copy.

## Testing the UI without the database

In `Program.cs`, use `new InMemoryInventoryData()` instead of `new DatabaseInventoryData()`.

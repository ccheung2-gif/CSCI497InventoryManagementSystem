using System.Collections.Generic;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Everything the forms need from the data layer. The UI only ever talks to
    /// this interface, so swapping the in-memory version for the real database
    /// version is a one-line change in Program.cs.
    ///
    /// Implementations should throw an exception when something goes wrong;
    /// the forms catch it and show the message to the user.
    /// </summary>
    public interface IInventoryData
    {
        /// <summary>Returns every item. Filtering/searching is done by the UI.</summary>
        List<InventoryItem> GetAllItems();

        /// <summary>Inserts a new item and returns the ID the database assigned to it.</summary>
        int AddItem(InventoryItem item);

        /// <summary>Saves changes to an existing item (matched by item.Id).</summary>
        void UpdateItem(InventoryItem item);

        /// <summary>Removes the item with the given ID.</summary>
        void DeleteItem(int id);

        /// <summary>Category names for the drop-downs.</summary>
        List<string> GetCategories();

        /// <summary>Supplier names for the drop-down.</summary>
        List<string> GetSuppliers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string AllCategories = "(All)";

        private readonly IInventoryData _data;
        private List<InventoryItem> _allItems = new List<InventoryItem>();

        public Form1(IInventoryData data)
        {
            InitializeComponent();
            _data = data;

            // Columns are defined in the designer; bind them to InventoryItem properties.
            dgvInventory.AutoGenerateColumns = false;
            colId.DataPropertyName = "Id";
            colName.DataPropertyName = "Name";
            colCategory.DataPropertyName = "Category";
            colQuantity.DataPropertyName = "Quantity";
            colReorder.DataPropertyName = "ReorderLevel";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colUnitPrice.DefaultCellStyle.Format = "C2";
            colLocation.DataPropertyName = "Location";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategoryFilter();
            RefreshItems();
        }

        // ---------------------------------------------------------------
        // Button / control handlers
        // ---------------------------------------------------------------

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            InventoryItem newItem = ShowItemDialog(null);
            if (newItem == null)
            {
                return;
            }

            int newId = 0;
            if (RunDataAction("add the item", () => newId = _data.AddItem(newItem)))
            {
                RefreshItems(newId);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        private void dgvInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedItem();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            InventoryItem selected = GetSelectedItem();
            if (selected == null)
            {
                return;
            }

            DialogResult answer = MessageBox.Show(this,
                "Delete \"" + selected.Name + "\"? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (answer == DialogResult.Yes &&
                RunDataAction("delete the item", () => _data.DeleteItem(selected.Id)))
            {
                RefreshItems();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategoryFilter();
            RefreshItems();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void chkLowStock_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // ---------------------------------------------------------------
        // Helpers
        // ---------------------------------------------------------------

        private void EditSelectedItem()
        {
            InventoryItem selected = GetSelectedItem();
            if (selected == null)
            {
                return;
            }

            InventoryItem edited = ShowItemDialog(selected);
            if (edited != null && RunDataAction("save the item", () => _data.UpdateItem(edited)))
            {
                RefreshItems(edited.Id);
            }
        }

        /// <summary>
        /// Opens the Add/Edit dialog. Pass null to add a new item.
        /// Returns the filled-in item, or null if the user cancelled.
        /// </summary>
        private InventoryItem ShowItemDialog(InventoryItem existing)
        {
            List<string> categories = null;
            List<string> suppliers = null;
            if (!RunDataAction("load categories and suppliers", () =>
                {
                    categories = _data.GetCategories();
                    suppliers = _data.GetSuppliers();
                }))
            {
                return null;
            }

            using (Form2 dialog = new Form2(categories, suppliers, existing))
            {
                return dialog.ShowDialog(this) == DialogResult.OK ? dialog.Item : null;
            }
        }

        private InventoryItem GetSelectedItem()
        {
            InventoryItem item = dgvInventory.CurrentRow == null
                ? null
                : dgvInventory.CurrentRow.DataBoundItem as InventoryItem;

            if (item == null)
            {
                MessageBox.Show(this, "Select an item first.", "No Item Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return item;
        }

        /// <summary>Reloads all items from the data layer, then re-applies the filters.</summary>
        private void RefreshItems(int? selectId = null)
        {
            List<InventoryItem> items = null;
            RunDataAction("load the inventory", () => items = _data.GetAllItems());
            _allItems = items ?? new List<InventoryItem>();

            ApplyFilters();

            if (selectId.HasValue)
            {
                SelectItem(selectId.Value);
            }
        }

        private void LoadCategoryFilter()
        {
            List<string> categories = null;
            RunDataAction("load categories", () => categories = _data.GetCategories());

            object previous = cboCategoryFilter.SelectedItem;

            cboCategoryFilter.SelectedIndexChanged -= cboCategoryFilter_SelectedIndexChanged;
            cboCategoryFilter.Items.Clear();
            cboCategoryFilter.Items.Add(AllCategories);
            if (categories != null)
            {
                cboCategoryFilter.Items.AddRange(categories.Cast<object>().ToArray());
            }

            int index = previous == null ? -1 : cboCategoryFilter.Items.IndexOf(previous);
            cboCategoryFilter.SelectedIndex = index >= 0 ? index : 0;
            cboCategoryFilter.SelectedIndexChanged += cboCategoryFilter_SelectedIndexChanged;
        }

        /// <summary>
        /// Shows only items matching the search text, category, and low-stock checkbox.
        /// Filtering is done in memory, so the data layer only needs a "get all" method.
        /// </summary>
        private void ApplyFilters()
        {
            string searchText = txtSearch.Text.Trim();
            string category = cboCategoryFilter.SelectedItem as string;
            bool filterCategory = category != null && category != AllCategories;
            bool lowStockOnly = chkLowStock.Checked;

            List<InventoryItem> visible = _allItems
                .Where(i => !filterCategory || string.Equals(i.Category, category, StringComparison.OrdinalIgnoreCase))
                .Where(i => !lowStockOnly || i.IsLowStock)
                .Where(i => searchText.Length == 0 || ItemMatches(i, searchText))
                .ToList();

            dgvInventory.DataSource = visible;
            lblItemCount.Text = visible.Count.ToString();
        }

        private static bool ItemMatches(InventoryItem item, string searchText)
        {
            string[] fields =
            {
                item.Id.ToString(), item.Name, item.Category, item.Description,
                item.Supplier, item.Location
            };

            return fields.Any(f => f != null &&
                f.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void SelectItem(int id)
        {
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                InventoryItem item = row.DataBoundItem as InventoryItem;
                if (item != null && item.Id == id)
                {
                    dgvInventory.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        /// <summary>
        /// Runs a data-layer call and shows a friendly error instead of crashing
        /// if it throws. Returns true if the call succeeded.
        /// </summary>
        private bool RunDataAction(string description, Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not " + description + ".\n\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}

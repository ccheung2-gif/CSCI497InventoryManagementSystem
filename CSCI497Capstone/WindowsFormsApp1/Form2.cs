using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Add / Edit Item dialog. It only collects and validates input; Form1 does
    /// the actual saving through the data layer.
    /// </summary>
    public partial class Form2 : Form
    {
        private readonly InventoryItem _existing;

        /// <summary>The item the user entered. Set only when the dialog returns OK.</summary>
        public InventoryItem Item { get; private set; }

        /// <param name="existing">The item to edit, or null to add a new one.</param>
        public Form2(IEnumerable<string> categories, IEnumerable<string> suppliers, InventoryItem existing)
        {
            InitializeComponent();
            _existing = existing;

            FillCombo(cboCategory, categories);
            FillCombo(cboSupplier, suppliers);

            // The database assigns IDs, so the user never types one.
            txtItemId.ReadOnly = true;
            txtItemId.TabStop = false;

            if (existing == null)
            {
                Text = "Add Item";
                txtItemId.Text = "(assigned on save)";
            }
            else
            {
                Text = "Edit Item";
                txtItemId.Text = existing.Id.ToString();
                txtName.Text = existing.Name;
                txtDescription.Text = existing.Description;
                txtQuantity.Text = existing.Quantity.ToString();
                txtUnitPrice.Text = existing.UnitPrice.ToString("0.00");
                SelectComboValue(cboCategory, existing.Category);
                SelectComboValue(cboSupplier, existing.Supplier);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                ShowInvalid(txtName, "Name is required.");
                return;
            }

            int quantity;
            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity < 0)
            {
                ShowInvalid(txtQuantity, "Quantity must be a whole number of 0 or more.");
                return;
            }

            decimal unitPrice;
            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Currency,
                                  CultureInfo.CurrentCulture, out unitPrice) || unitPrice < 0)
            {
                ShowInvalid(txtUnitPrice, "Unit price must be a number of 0 or more.");
                return;
            }

            // Start from a copy of the original so fields this form doesn't edit
            // (e.g. ReorderLevel, Location) are preserved on update.
            InventoryItem item = _existing != null ? _existing.Clone() : new InventoryItem();
            item.Name = name;
            item.Category = cboCategory.SelectedItem as string;
            item.Description = txtDescription.Text.Trim();
            item.Supplier = cboSupplier.SelectedItem as string;
            item.Quantity = quantity;
            item.UnitPrice = unitPrice;

            Item = item;
            DialogResult = DialogResult.OK;
        }

        private void ShowInvalid(Control control, string message)
        {
            MessageBox.Show(this, message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
        }

        private static void FillCombo(ComboBox combo, IEnumerable<string> values)
        {
            combo.Items.Clear();
            if (values != null)
            {
                combo.Items.AddRange(values.Cast<object>().ToArray());
            }
        }

        /// <summary>Selects a value, adding it first if it isn't in the list.</summary>
        private static void SelectComboValue(ComboBox combo, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            if (!combo.Items.Contains(value))
            {
                combo.Items.Add(value);
            }

            combo.SelectedItem = value;
        }
    }
}

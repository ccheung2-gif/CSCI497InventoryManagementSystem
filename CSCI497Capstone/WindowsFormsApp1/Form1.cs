using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        /// <summary>
        /// Shows only the grid rows where any cell contains the search text
        /// (case-insensitive). An empty search shows every row.
        /// </summary>
        private void ApplySearchFilter()
        {
            string searchText = txtSearch.Text.Trim();
            int visibleCount = 0;

            dgvInventory.ClearSelection();
            dgvInventory.CurrentCell = null;

            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                bool matches = searchText.Length == 0 || RowContains(row, searchText);
                row.Visible = matches;

                if (matches)
                {
                    visibleCount++;
                }
            }

            lblItemCount.Text = visibleCount.ToString();
        }

        private static bool RowContains(DataGridViewRow row, string searchText)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                string value = Convert.ToString(cell.Value);
                if (value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using System;
using System.Windows.Forms;
using WindowsFormsApp1.Data;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IInventoryData data;
            try
            {
                DatabaseSetup.Initialize();
                data = new DatabaseInventoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the inventory database.\n\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // To test the UI without a database, use: data = new InMemoryInventoryData();
            Application.Run(new Form1(data));
        }
    }
}

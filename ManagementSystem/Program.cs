using System;
using System.Windows.Forms;
using ManagementSystem.Views;

namespace ManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
            System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Views.MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application failed to start: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
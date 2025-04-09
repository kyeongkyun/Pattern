using System.Windows.Forms;

namespace MVPPattern
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var form = new Form1();
            var presenter = new UserPresenter(form);
            Application.Run(form);
        }
    }
}
using System;
using System.Windows.Forms;
using CodeReviewer.Config;

namespace MainApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Configurator.WireUp();
            Application.Run(new TestPython());
        }
    }
}
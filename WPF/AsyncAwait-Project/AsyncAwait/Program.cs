using System;
using System.Windows;
using Views;

namespace AsyncAwait
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MainWindow());
        }
    }
}

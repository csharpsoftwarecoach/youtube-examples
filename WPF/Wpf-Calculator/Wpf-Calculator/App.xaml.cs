using System.Windows;
using Views;

namespace WpfCalculator
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var view = new MainWindowView();
            view.ShowDialog();
        }
    }
}

using System.Windows;
using WpfMvvmIntroduction.Views;

namespace WpfMvvmIntroduction.WpfApplication
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindowView = new MainWindowView();

            mainWindowView.Show();
        }
    }
}

using System.Windows;
using WpfMvvmIntroduction.ViewModels;

namespace WpfMvvmIntroduction.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}

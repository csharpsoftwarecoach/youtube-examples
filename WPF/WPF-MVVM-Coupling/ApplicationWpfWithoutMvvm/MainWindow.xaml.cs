using System.Windows;

namespace ApplicationWpfWithoutMvvm
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, $"Guten Tag Frau/Herr {txtName.Text}!", "Hinweis");
        }
    }
}

using Infrastructure.Presentation.Model;

namespace WpfMvvmIntroduction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Name { get; set; } = "Stefan";

        public MainWindowViewModel()
        {

        }
    }
}

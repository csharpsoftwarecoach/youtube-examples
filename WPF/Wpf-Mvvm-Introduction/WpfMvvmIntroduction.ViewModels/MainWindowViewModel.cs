using Infrastructure.Presentation.Model;
using System.Windows.Input;

namespace WpfMvvmIntroduction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Name { get; set; } = "Stefan";

        public ICommand StartCommand { get; }

        public MainWindowViewModel()
        {
            StartCommand = new Command(StartCommandCallback);
        }

        private void StartCommandCallback()
        {
            Name = "Start";

            OnPropertyChanged(nameof(Name));
        }
    }
}

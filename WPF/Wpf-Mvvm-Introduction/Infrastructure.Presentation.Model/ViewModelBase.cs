using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Infrastructure.Presentation.Model
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected virtual ICommands Commands { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            Commands = new Commands();
        }
    }
}

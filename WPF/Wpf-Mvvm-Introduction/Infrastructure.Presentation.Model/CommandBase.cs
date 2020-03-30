using System;
using System.Windows.Input;

namespace Infrastructure.Presentation.Model
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        public void Update()
        {
            this.CanExecuteChanged?.Invoke(this, null);
        }
    }
}

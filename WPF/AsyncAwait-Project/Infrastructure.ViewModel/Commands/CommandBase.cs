using System;
using System.Threading;
using System.Windows.Threading;

namespace Infrastructure.ViewModel
{
    public abstract class CommandBase : IDelegateCommand
    {
        private SynchronizationContext _uiContext;

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        public void Update()
        {
            if (this._uiContext == null && DispatcherSynchronizationContext.Current != null)
            {
                this._uiContext = DispatcherSynchronizationContext.Current;
            }

            if (this._uiContext != null && SynchronizationContext.Current != this._uiContext)
            {
                this._uiContext.Send(this.update, this);
            }
            else
            {
                this.update(null);
            }
        }

        void update(object state)
        {
            this.CanExecuteChanged?.Invoke(this, null);
        }
    }
}

using System;

namespace Infrastructure.Presentation.Model
{
    internal class Command<T> : CommandBase
    {
        private Func<T, bool> _canExecute;
        private Action<T> _execute;

        public Command(Action<T> execute)
            : this(execute, t => true)
        {
        }

        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            this._canExecute = canExecute;
            this._execute = execute;
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is T)
            {
                return this._canExecute((T)parameter);
            }
            else
            {
                return this._canExecute(default(T));
            }
        }

        public override void Execute(object parameter)
        {
            if (parameter is T)
            {
                this._execute.Invoke((T)parameter);
            }
            else
            {
                this._execute.Invoke(default(T));
            }
        }
    }
}

using System;

namespace Infrastructure.Presentation.Model
{
    public sealed class Command : CommandBase
    {
        private Func<bool> _canExecute;
        private Action _execute;

        public Command(Action execute)
            : this(execute, () => true)
        {

        }
        public Command(Action execute, Func<bool> canExecute)
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
            return this._canExecute();
        }

        public override void Execute(object parameter)
        {
            this._execute.Invoke();
        }


    }
}

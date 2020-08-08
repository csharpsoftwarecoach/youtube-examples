using System;

namespace Infrastructure.ViewModel
{
    public class DelegateCommand<TArgs> : CommandBase
    {
        private readonly Func<TArgs, bool> _canExecute;
        private readonly Action<TArgs> _execute;

        public DelegateCommand(Action<TArgs> execute)
            : this(execute, t => true)
        {
        }

        public DelegateCommand(Action<TArgs> execute, Func<TArgs, bool> canExecute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }
        public DelegateCommand(Func<TArgs, bool> canExecute)
        {
            this._canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is TArgs)
            {
                return this._canExecute((TArgs)parameter);
            }
            else
            {
                return this._canExecute(default(TArgs));
            }
        }

        public override void Execute(object parameter)
        {
            if (parameter is TArgs)
            {
                this._execute((TArgs)parameter);
            }
            else
            {
                this._execute(default(TArgs));
            }
        }
    }

    public class DelegateCommand : CommandBase
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        public DelegateCommand(Action execute) : this(execute, null)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public override void Execute(object parameter)
        {
            _execute();
        }

    }
}

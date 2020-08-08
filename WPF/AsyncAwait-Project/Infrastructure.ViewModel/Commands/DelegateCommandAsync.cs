using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public class DelegateCommandAsync<TArgs> : CommandBase
    {
        private Func<TArgs, bool> _canExecute;
        private Func<TArgs, CancellationTokenSource, Task> _execute;
        protected CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

        public DelegateCommandAsync(Func<TArgs, CancellationTokenSource, Task> execute)
            : this(execute, t => true)
        {
        }

        public DelegateCommandAsync(Func<TArgs, CancellationTokenSource, Task> execute, Func<TArgs, bool> canExecute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }
        public DelegateCommandAsync(Func<TArgs, bool> canExecute)
        {
            this._canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is TArgs)
            {
                return this.canExecute((TArgs)parameter).Result;
            }
            else
            {
                return this.canExecute(default(TArgs)).Result;
            }
        }
#pragma warning disable 4014
        public override void Execute(object parameter)
        {
            if (parameter is TArgs)
            {
                this.execute((TArgs)parameter);
            }
            else
            {
                this.execute(default(TArgs));
            }
        }

#pragma warning disable 1998
        private async Task<bool> canExecute(TArgs parameter)
        {
            return this._canExecute(parameter);
        }

        protected virtual async Task execute(TArgs parameter)
        {
            await this._execute(parameter, CancellationToken);
        }
    }

    public class DelegateCommandAsync : CommandBase
    {
        private Func<bool> _canExecute;
        private Func<CancellationTokenSource, Task> _execute;
        protected CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

        public DelegateCommandAsync(Func<CancellationTokenSource, Task> execute)
            : this(execute, () => true)
        {
        }

        public DelegateCommandAsync(Func<CancellationTokenSource, Task> execute, Func<bool> canExecute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }
        public DelegateCommandAsync(Func<bool> canExecute)
        {
            this._canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return this.canExecute().Result;
        }
#pragma warning disable 4014
        public override void Execute(object parameter)
        {
            this.execute();
        }

#pragma warning disable 1998
        private async Task<bool> canExecute()
        {
            return this._canExecute();
        }

        protected virtual async Task execute()
        {
            await this._execute(CancellationToken);
        }
    }
}

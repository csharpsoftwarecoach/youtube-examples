using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public class DelegateCommandAsync<TArgs, TReport> : DelegateCommandAsync<TArgs>
    {
        private Func<TArgs, Action<TReport>, CancellationTokenSource, Task> _executeWithReport;
        private Action<TReport> _reportAction;
        private IProgress<TReport> _reporter;

        public DelegateCommandAsync(Func<TArgs, Action<TReport>, CancellationTokenSource, Task> execute, Action<TReport> reportAction)
           : this(execute, reportAction, t => true)
        {
        }

        public DelegateCommandAsync(Func<TArgs, Action<TReport>, CancellationTokenSource, Task> execute, Action<TReport> reportAction, Func<TArgs, bool> canExecute)
            : base(canExecute)
        {
            _executeWithReport = execute;
            _reportAction = reportAction;
        }

#pragma warning disable 0114
        protected async Task execute(TArgs parameter)
        {
            await _executeWithReport(parameter, _reporter.Report, CancellationToken);
        }

#pragma warning disable 4014
        public override void Execute(object parameter)
        {
            if (_reporter == null)
            {
                _reporter = new Progress<TReport>(_reportAction);
            }

            if (parameter is TArgs)
            {
                execute((TArgs)parameter);
            }
            else
            {
                execute(default);
            }
        }
    }

    public class DelegateCommandAsyncProgress<TReport> : DelegateCommandAsync
    {
        private Func<Action<TReport>, CancellationTokenSource, Task> _executeWithReport;
        private Action<TReport> _reportAction;
        private IProgress<TReport> _reporter;

        public DelegateCommandAsyncProgress(Func<Action<TReport>, CancellationTokenSource, Task> execute, Action<TReport> reportAction)
           : this(execute, reportAction, () => true)
        {
        }

        public DelegateCommandAsyncProgress(Func< Action<TReport>, CancellationTokenSource, Task> execute, Action<TReport> reportAction, Func<bool> canExecute)
            : base(canExecute)
        {
            _executeWithReport = execute;
            _reportAction = reportAction;
        }

#pragma warning disable 0114
        protected async Task execute()
        {
            await _executeWithReport(_reporter.Report, CancellationToken);
        }

#pragma warning disable 4014
        public override void Execute(object parameter)
        {
            if (_reporter == null)
            {
                _reporter = new Progress<TReport>(_reportAction);
            }

            execute();
        }
    }
}

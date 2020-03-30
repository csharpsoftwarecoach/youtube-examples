using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Infrastructure.Presentation.Model
{
    public interface ICommands
    {
        ICommand Get(Action execute, Func<bool> canExecute = null, [CallerMemberName] string key = null);

        ICommand Get<T>(Action<T> execute, Func<T, bool> canExecute = null, [CallerMemberName] string key = null);

        void Add(ICommand command, object key);

        void Remove(object key);

        void Update();
    }
}

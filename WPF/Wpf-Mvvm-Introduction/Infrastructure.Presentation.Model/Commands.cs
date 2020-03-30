using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Infrastructure.Presentation.Model
{
    public class Commands : ICommands
    {
        private Dictionary<object, ICommand> _commands = new Dictionary<object, ICommand>();

        public ICommand Get(Action execute, Func<bool> canExecute = null, string key = null)
        {
            if (!_commands.ContainsKey(key))
            {
                if (canExecute == null)
                {
                    this._commands.Add(key, new Command(execute, () => true));
                }
                else
                {
                    this._commands.Add(key, new Command(execute, canExecute));
                }
            }
            return this._commands[key];
        }

        public ICommand Get<T>(Action<T> execute, Func<T, bool> canExecute = null, string key = null)
        {
            if (!_commands.ContainsKey(key))
            {
                if (canExecute == null)
                {
                    this._commands.Add(key, new Command<T>(execute, (t) => true));
                }
                else
                {
                    this._commands.Add(key, new Command<T>(execute, canExecute));
                }
            }
            return this._commands[key];
        }

        public void Add(ICommand command, object key)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            this._commands[key] = command;
        }


        public void Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (this._commands.ContainsKey(key))
            {
                this._commands.Remove(key);
            }
        }

        public void Update()
        {
            foreach (CommandBase cmd in this._commands.Values.Where(c => c is CommandBase))
            {
                cmd.Update();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Noneb.Core.Game.Commands
{
    public interface ICommandExecutionService
    {
        IObservable<Unit> Do(ICommand command);
        bool TryUndo(out IObservable<Unit> undoCommand);
    }

    public class CommandExecutionService : ICommandExecutionService
    {
        private readonly Stack<ICommand> _commands;

        public CommandExecutionService()
        {
            _commands = new Stack<ICommand>();
        }

        public IObservable<Unit> Do(ICommand command)
        {
            if (command.CanUndo)
                _commands.Push(command);
            else
                _commands.Clear();

            return command.Do();
        }

        public bool TryUndo(out IObservable<Unit> undoCommand)
        {
            if (_commands.Any())
            {
                undoCommand = _commands.Pop().Undo();
                return true;
            }

            undoCommand = default;
            return false;
        }
    }
}
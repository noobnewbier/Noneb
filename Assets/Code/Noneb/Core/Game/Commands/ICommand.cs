using System;

namespace Noneb.Core.Game.Commands
{
    public interface ICommand
    {
        IObservable<UniRx.Unit> Do();
        IObservable<UniRx.Unit> Undo();
        bool CanUndo { get; }
    }
}
using System;

namespace Noneb.Core.Game.Common.Commands
{
    public interface ICommand
    {
        IObservable<UniRx.Unit> Do();
        IObservable<UniRx.Unit> Undo();
    }
}
using System;
using UniRx;

namespace Noneb.Core.Game.Common.Loaders
{
    public interface ILoader
    {
        void LoadAndForget();
        IObservable<Unit> LoadObservable();
    }
}
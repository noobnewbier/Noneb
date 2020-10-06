using System;
using UniRx;

namespace Main.Core.Game.Common.Loaders
{
    public interface ILoader
    {
        void LoadAndForget();
        IObservable<Unit> LoadObservable();
    }
}
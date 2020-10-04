using System;
using UniRx;

namespace Common.Loaders
{
    public interface ILoader
    {
        void LoadAndForget();
        IObservable<Unit> LoadObservable();
    }
}
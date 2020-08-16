using System;
using UnityEngine;

namespace Common.Loaders
{
    public interface ILoader
    {
        void LoadAndForget();
        IObservable<UniRx.Unit> LoadObservable();
    }
}
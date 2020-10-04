using System;

namespace Experiment.CrossPlatformLiveData.Internal.Model
{
    internal interface IInternalObserverHolder
    {
        int Id { get; set; }
        Type Type { get; }
    }
}
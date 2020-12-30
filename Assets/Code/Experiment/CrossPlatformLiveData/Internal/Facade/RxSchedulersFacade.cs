using Experiment.NoobUniRxPlugin;
using UniRx;

namespace Experiment.CrossPlatformLiveData.Internal.Facade
{
    internal class RxSchedulersFacade : IRxSchedulersFacade
    {
        public IScheduler Io() => NoobSchedulers.ThreadPool;

        public IScheduler Ui() => NoobSchedulers.MainThread;
    }
}
using UniRx;

namespace Experiment.CrossPlatformLiveData.Internal.Facade
{
    /// <summary>
    /// <inheritdoc cref="IRxSchedulersFacade" />
    /// </summary>
    internal class RxSchedulersFacade : IRxSchedulersFacade
    {
        /// <summary>
        /// Schedulers pool with smart creation and re-use
        /// <inheritdoc cref="IRxSchedulersFacade.Io" />
        /// </summary>
        public IScheduler Io()
        {
            return Scheduler.DefaultSchedulers.AsyncConversions;
        }

        /// <summary>
        /// Used for synchronizing with UI thread
        /// <inheritdoc cref="IRxSchedulersFacade.Ui" />
        /// </summary>
        public IScheduler Ui()
        {
            return Scheduler.MainThread;
        }
    }
}
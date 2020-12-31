using UniRx;

namespace Experiment.NoobUniRxPlugin
{
    //be aware this does not deal with Unity specific schedulers
    public static class NoobSchedulers
    {
        private static IScheduler _immediate = Scheduler.Immediate;
        private static IScheduler _threadPool = Scheduler.ThreadPool;
        private static IScheduler _currentThread = Scheduler.CurrentThread;
        private static IScheduler _mainThread = Scheduler.MainThread;

        public static IScheduler MainThread => _mainThread;
        public static IScheduler CurrentThread => _currentThread;
        public static IScheduler ThreadPool => _threadPool;
        public static IScheduler Immediate => _immediate;

        internal static void ToDefaultMode()
        {
            _immediate = Scheduler.Immediate;
            _threadPool = Scheduler.ThreadPool;
            _currentThread = Scheduler.CurrentThread;
            _mainThread = Scheduler.MainThread;
            
#if !UniRxLibrary
            Scheduler.SetDefaultForUnity();
#endif
        }

        internal static void ToTestMode()
        {
            _immediate = Scheduler.Immediate;
            _threadPool = Scheduler.Immediate;
            _currentThread = Scheduler.Immediate;
            _mainThread = Scheduler.Immediate;
            
#if !UniRxLibrary
            Scheduler.SetDefaultForUnity();
#endif            
        }
    }
}
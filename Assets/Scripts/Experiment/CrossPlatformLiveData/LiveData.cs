using System;
using System.Collections.Concurrent;
using System.Threading;
using Experiment.CrossPlatformLiveData.Internal.Facade;
using UniRx;

/*
 * The original implementer did a weird job translating this,
 * probably because he is not really capturing the whole thing but instead mimicking it.
 * Issue so far:
 *     1. doesn't make sense to use subscribe, should implement "Observe" instead
 *     2. Not sure about thread safety...
 *     3. ATTEMPTED TO FIX: try to use versioning instead of instance matching(which makes more sense anyway)
 *
 * consider to fully(or partly) translate the whole thing from Android instead of using this:
 * https://github.com/aosp-mirror/platform_frameworks_support/blob/b9cd83371e928380610719dfbf97c87c58e80916/lifecycle/lifecycle-livedata-core/src/main/java/androidx/lifecycle/LiveData.java
 */

namespace Experiment.CrossPlatformLiveData
{
    public class LiveData<T> : ILiveData<T>
    {
        private readonly BehaviorSubject<T> _subject;
        private readonly IRxSchedulersFacade _rxSchedulers = new RxSchedulersFacade();
        private readonly ConcurrentBag<LiveDataObserverWrapper> _observers;

        #region Added by Noob

        private int _version;
        private const int StartVersion = -1;

        #endregion

        /// <summary>
        /// First emitted value will be type default
        /// </summary>
        public LiveData()
        {
            _subject = new BehaviorSubject<T>(default);
            _version = StartVersion;
        }

        /// <summary>
        /// Emit first value with provided default
        /// </summary>
        /// <param name="initValue">Initial emitted value</param>
        public LiveData(T initValue)
        {
            _subject = new BehaviorSubject<T>(initValue);
            _version = StartVersion + 1;
        }

        /// <summary>
        /// Emit first value with provided default
        /// </summary>
        /// <param name="initValue">Initial emitted value</param>
        /// <param name="rxSchedulers">Use custom IRxSchedulersFacade implementation</param>
        public LiveData(T initValue, IRxSchedulersFacade rxSchedulers)
        {
            _subject = new BehaviorSubject<T>(initValue);
            _rxSchedulers = rxSchedulers;
            _version = StartVersion + 1;
        }

        public T Value { get; private set; }

        /// <summary>
        /// Post new value asynchronously
        /// </summary>
        /// <param name="value"></param>
        public void PostValue(T value)
        {
            Interlocked.Increment(ref _version);
            _subject.OnNext(value);
        }

        /// <summary>
        /// Subscribes to LiveData, if allowDuplicatesInSequenceAndOnResume flag is set, duplicates in sequence will be allowed
        /// and last value re-emitted on onResume event
        /// </summary>
        /// <param name="onNext">Emits only non null objects</param>
        /// <param name="onError"></param>
        /// <param name="onCompleted"></param>
        /// <returns>IDisposable</returns>
        public IDisposable Subscribe(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            var observerWrapper = new LiveDataObserverWrapper(Observer.Create(onNext, onError, onCompleted));
            return _subject.Select(d => new VersionedData(d, _version))
                .SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(observerWrapper);
        }

        #region Added by Noob

        /// <summary>
        /// Subscribes to LiveData, if allowDuplicatesInSequenceAndOnResume flag is set, duplicates in sequence will be allowed
        /// and last value re-emitted on onResume event
        /// </summary>
        /// <param name="onNext">Emits only non null objects</param>
        /// <param name="onError"></param>
        /// <returns>IDisposable</returns>
        public IDisposable Subscribe(Action<T> onNext, Action<Exception> onError)
        {
            var observerWrapper = new LiveDataObserverWrapper(Observer.Create(onNext, onError));
            return _subject.Select(d => new VersionedData(d, _version))
                .SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(observerWrapper);
        }

        /// <summary>
        /// Subscribes to LiveData, if allowDuplicatesInSequenceAndOnResume flag is set, duplicates in sequence will be allowed
        /// and last value re-emitted on onResume event
        /// </summary>
        /// <param name="onNext">Emits only non null objects</param>
        /// <returns>IDisposable</returns>
        public IDisposable Subscribe(Action<T> onNext)
        {
            var observerWrapper = new LiveDataObserverWrapper(Observer.Create(onNext));
            return _subject.Select(d => new VersionedData(d, _version))
                .SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(observerWrapper);
        }

        private class VersionedData
        {
            public VersionedData(T data, int version)
            {
                Data = data;
                Version = version;
            }

            public T Data { get; }
            public int Version { get; }
        }

        private class LiveDataObserverWrapper : IObserver<VersionedData>
        {
            private int LastVersion { get; set; } = StartVersion;
            private readonly IObserver<T> _observer;

            public LiveDataObserverWrapper(IObserver<T> observer)
            {
                _observer = observer;
            }


            public void OnCompleted()
            {
                _observer.OnCompleted();
            }

            public void OnError(Exception error)
            {
                _observer.OnError(error);
            }

            public void OnNext(VersionedData value)
            {
                if (LastVersion >= value.Version)
                {
                    return;
                }

                LastVersion = value.Version;

                _observer.OnNext(value.Data);
            }
        }

        #endregion
    }
}
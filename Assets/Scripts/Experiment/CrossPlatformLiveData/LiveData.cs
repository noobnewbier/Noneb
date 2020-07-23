using System;
using Experiment.CrossPlatformLiveData.Internal.Facade;
using UniRx;

namespace Experiment.CrossPlatformLiveData
{
    public class LiveData<T> : ILiveData<T>
    {
        private readonly BehaviorSubject<T> _subject;
        private readonly IRxSchedulersFacade _rxSchedulers = new RxSchedulersFacade();
        private readonly bool _allowDuplicatesInSequenceAndOnResumeFlag;
        private T _lastEmitted;

        /// <summary>
        /// First emitted value will be type default
        /// </summary>
        /// <param name="allowDuplicatesInSequenceAndOnResume">
        /// If set emitting the same value one after another is allowed,
        /// Will also cause value re-emission onResume lifecycle event
        /// </param>
        public LiveData(bool allowDuplicatesInSequenceAndOnResume = false)
        {
            _subject = new BehaviorSubject<T>(default);
            _allowDuplicatesInSequenceAndOnResumeFlag = allowDuplicatesInSequenceAndOnResume;
        }

        /// <summary>
        /// Emit first value with provided default
        /// </summary>
        /// <param name="initValue">Initial emitted value</param>
        /// <param name="allowDuplicatesInSequenceAndOnResume">
        /// If set emitting the same value one after another is allowed,
        /// Will also cause value re-emission onResume lifecycle event
        /// </param>
        public LiveData(T initValue, bool allowDuplicatesInSequenceAndOnResume = false)
        {
            _subject = new BehaviorSubject<T>(initValue);
            _allowDuplicatesInSequenceAndOnResumeFlag = allowDuplicatesInSequenceAndOnResume;
        }

        /// <summary>
        /// Emit first value with provided default
        /// </summary>
        /// <param name="initValue">Initial emitted value</param>
        /// <param name="rxSchedulers">Use custom IRxSchedulersFacade implementation</param>
        /// <param name="allowDuplicatesInSequenceAndOnResume">
        /// If set emitting the same value one after another is allowed,
        /// by default duplicates can be emitted only if there was another value in-between them in a sequence.
        /// Will also cause value re-emission onResume lifecycle event
        /// </param>
        public LiveData(T initValue, IRxSchedulersFacade rxSchedulers, bool allowDuplicatesInSequenceAndOnResume = false)
        {
 
            _subject = new BehaviorSubject<T>(initValue);
            _rxSchedulers = rxSchedulers;
            _allowDuplicatesInSequenceAndOnResumeFlag = allowDuplicatesInSequenceAndOnResume;
        }

        public T Value { get; private set; }

        /// <summary>
        /// Post new value asynchronously
        /// </summary>
        /// <param name="value"></param>
        public void PostValue(T value)
        {
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
            return _subject.SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(
                    obj =>
                    {
                        if (obj != null && _allowDuplicatesInSequenceAndOnResumeFlag)
                        {
                            Value = obj;
                            onNext.Invoke(obj);
                        }
                        else
                        {
                            if (obj != null && !obj.Equals(_lastEmitted))
                            {
                                Value = obj;
                                onNext.Invoke(obj);
                            }

                            _lastEmitted = obj;
                        }
                    },
                    onError,
                    onCompleted
                );
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
            return _subject.SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(
                    obj =>
                    {
                        if (obj != null && _allowDuplicatesInSequenceAndOnResumeFlag)
                        {
                            Value = obj;
                            onNext.Invoke(obj);
                        }
                        else
                        {
                            if (obj != null && !obj.Equals(_lastEmitted))
                            {
                                Value = obj;
                                onNext.Invoke(obj);
                            }

                            _lastEmitted = obj;
                        }
                    },
                    onError
                );
        }

        /// <summary>
        /// Subscribes to LiveData, if allowDuplicatesInSequenceAndOnResume flag is set, duplicates in sequence will be allowed
        /// and last value re-emitted on onResume event
        /// </summary>
        /// <param name="onNext">Emits only non null objects</param>
        /// <returns>IDisposable</returns>
        public IDisposable Subscribe(Action<T> onNext)
        {
            return _subject.SubscribeOn(_rxSchedulers.Io())
                .ObserveOn(_rxSchedulers.Ui())
                .Subscribe(
                    obj =>
                    {
                        if (obj != null && _allowDuplicatesInSequenceAndOnResumeFlag)
                        {
                            Value = obj;
                            onNext.Invoke(obj);
                        }
                        else
                        {
                            if (obj != null && !obj.Equals(_lastEmitted))
                            {
                                Value = obj;
                                onNext.Invoke(obj);
                            }

                            _lastEmitted = obj;
                        }
                    }
                );
        }

        #endregion
    }
}
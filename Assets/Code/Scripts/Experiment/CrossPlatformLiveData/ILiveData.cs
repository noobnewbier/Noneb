using System;

namespace Experiment.CrossPlatformLiveData
{
    /// <summary>
    /// LiveData interface
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface ILiveData<T>
    {
        T Value { get; }
        void PostValue(T value);

        /// <summary>
        /// Subscribes to LiveData, if allowDuplicatesInSequenceAndOnResume flag is set, duplicates in sequence will be allowed
        /// and last value re-emitted on onResume event
        /// </summary>
        /// <param name="onNext">Emits only non null objects</param>
        /// <param name="onError"></param>
        /// <param name="onCompleted"></param>
        /// <returns>IDisposable</returns>
        IDisposable Subscribe(Action<T> onNext, Action<Exception> onError, Action onCompleted);

        #region Added by Noob

        IDisposable Subscribe(Action<T> onNext, Action<Exception> onError);
        IDisposable Subscribe(Action<T> onNext);

        #endregion
    }
}
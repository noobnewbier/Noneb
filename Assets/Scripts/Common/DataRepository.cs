using System;
using UniRx;

namespace Common
{
    public interface IDataGetRepository<out T> where T : class
    {
        IObservable<T> GetObservableStream();
        IObservable<T> GetMostRecent();
    }

    public interface IDataSetRepository<in T> where T : class
    {
        void Set(T value);
    }

    public abstract class DataRepository<T> : IDataGetRepository<T>, IDataSetRepository<T> where T : class
    {
        private readonly ReplaySubject<T> _subject;
        private IObservable<T> _single;

        protected DataRepository()
        {
            //give the current value if there's any(including null)
            _subject = new ReplaySubject<T>(1);
            _single = Observable.Throw<T>(new InvalidOperationException($"Value is not set yet for {GetType().Name}"));
        }

        public IObservable<T> GetObservableStream()
        {
            return _subject;
        }

        public IObservable<T> GetMostRecent()
        {
            return _single;
        }

        public void Set(T value)
        {
            _subject.OnNext(value);
            _single = Observable.Return(value);
        }
    }
}
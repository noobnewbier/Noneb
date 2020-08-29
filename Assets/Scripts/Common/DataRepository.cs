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
        private readonly BehaviorSubject<T> _subject;
        private IObservable<T> _single;

        protected DataRepository()
        {
            _subject = new BehaviorSubject<T>(default);
            _single = Observable.Throw<T>(new InvalidOperationException("Value is not set yet"));
        }

        public IObservable<T> GetObservableStream()
        {
            return _subject.Where(t => t != null);
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
using System;
using UniRx;

namespace Noneb.Core.Game.Common
{
    public interface IDataRepository<T> : IDataGetRepository<T>, IDataSetRepository<T>
    {
    }

    public interface IDataGetRepository<out T>
    {
        IObservable<T> GetObservableStream();
        IObservable<T> GetMostRecent();
    }

    public interface IDataSetRepository<in T>
    {
        IObservable<Unit> Set(T value);
    }

    public class DataRepository<T> : IDataRepository<T>
    {
        private readonly ReplaySubject<T> _subject;
        private IObservable<T> _single;

        public DataRepository()
        {
            //give the current value if there's any(including null)
            _subject = new ReplaySubject<T>(1);
            _single = Observable.Throw<T>(new InvalidOperationException($"Value is not set yet for {GetType().Name}"));
        }

        public IObservable<T> GetObservableStream() => _subject;

        public IObservable<T> GetMostRecent() => _single;

        public IObservable<Unit> Set(T value)
        {
            _subject.OnNext(value);
            _single = Observable.Return(value);

            return Observable.ReturnUnit();
        }
    }
}
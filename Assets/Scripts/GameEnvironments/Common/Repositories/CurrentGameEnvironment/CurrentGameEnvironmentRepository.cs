using System;
using GameEnvironments.Common.Data;
using UniRx;
using UnityUtils.ScriptableReference;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    public interface ICurrentGameEnvironmentGetRepository
    {
        IObservable<GameEnvironment> GetObservableStream();
        IObservable<GameEnvironment> GetMostRecent();
    }

    public interface ICurrentGameEnvironmentSetRepository
    {
        void Set(GameEnvironment gameEnvironment);
    }

    public class CurrentGameEnvironmentRepository : ICurrentGameEnvironmentGetRepository, ICurrentGameEnvironmentSetRepository
    {
        //might not need this now, a plain reference would probably do the trick...
        private readonly RuntimeReference<GameEnvironment> _runtimeReference;
        private readonly BehaviorSubject<GameEnvironment> _subject;
        private IObservable<GameEnvironment> _single;

        public CurrentGameEnvironmentRepository(RuntimeReference<GameEnvironment> runtimeReference)
        {
            _runtimeReference = runtimeReference;

            _subject = new BehaviorSubject<GameEnvironment>(default);
        }

        public IObservable<GameEnvironment> GetObservableStream()
        {
            //we are using a behaviour subject so the most recent value is emitted when subscribe, but we don't want the default value(null) to be included
            return _subject.Where(e => e != null);
        }

        public IObservable<GameEnvironment> GetMostRecent()
        {
            return _single;
        }

        public void Set(GameEnvironment gameEnvironment)
        {
            _runtimeReference.CurrentReference = gameEnvironment;

            _single = Observable.Return(gameEnvironment);
            _subject.OnNext(_runtimeReference.CurrentReference);
        }
    }
}
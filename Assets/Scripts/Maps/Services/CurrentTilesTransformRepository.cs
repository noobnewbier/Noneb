using System;
using System.Collections.Generic;
using Common.Providers;
using GameEnvironments.Load.Tiles;
using UniRx;
using UnityEngine;

namespace Maps.Services
{
    public interface ICurrentTilesTransformSetRepository
    {
        void SetTransformProvider(IObjectProvider<IList<Transform>> transformProvider);
    }

    public interface ICurrentTilesTransformGetRepository
    {
        IObservable<IList<Transform>> GetMostRecent();
        IObservable<IList<Transform>> GetObservableStream();
    }

    public class CurrentTilesTransformRepository : ICurrentTilesTransformSetRepository, ICurrentTilesTransformGetRepository, IDisposable
    {
        private readonly BehaviorSubject<IList<Transform>> _subject;
        private readonly IDisposable _disposable;
        private IObjectProvider<IList<Transform>> _tilesTransformProvider;
        private IObservable<IList<Transform>> _single;

        public CurrentTilesTransformRepository(IMapLoadService mapLoadService)
        {
            _subject = new BehaviorSubject<IList<Transform>>(default);

            _disposable = mapLoadService
                .GetFinishedLoadingEventStream()
                .Subscribe( _ => UpdateValues());
        }

        public void SetTransformProvider(IObjectProvider<IList<Transform>> transformProvider)
        {
            _tilesTransformProvider = transformProvider;
            
            UpdateValues();
        }

        public IObservable<IList<Transform>> GetMostRecent()
        {
            return _single;
        }

        public IObservable<IList<Transform>> GetObservableStream()
        {
            return _subject.Where(t => t != null);
        }

        private void UpdateValues()
        {
            var transforms = _tilesTransformProvider.Provide();
            
            _subject.OnNext(transforms);
            _single = Observable.Return(transforms);
        }

        public void Dispose()
        {
            _subject?.Dispose();
            _disposable?.Dispose();
        }
    }
}
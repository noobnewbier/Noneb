using System;
using System.Collections.Generic;
using Common;
using Common.Providers;
using GameEnvironments.Load.Tiles;
using UniRx;
using UnityEngine;

namespace Maps.Services
{
    public interface ICurrentTilesTransformProviderSetRepository : IDataSetRepository<IObjectProvider<IList<Transform>>>
    {
    }

    public interface ICurrentTilesTransformGetRepository : IDataGetRepository<IList<Transform>>
    {
    }

    public class CurrentTilesTransformRepository : ICurrentTilesTransformProviderSetRepository, ICurrentTilesTransformGetRepository, IDisposable
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
                .Subscribe(_ => UpdateValues());
        }

        public IObservable<IList<Transform>> GetMostRecent()
        {
            return _single;
        }

        public IObservable<IList<Transform>> GetObservableStream()
        {
            return _subject.Where(t => t != null);
        }

        public void Set(IObjectProvider<IList<Transform>> transformProvider)
        {
            _tilesTransformProvider = transformProvider;
        }

        public void Dispose()
        {
            _subject?.Dispose();
            _disposable?.Dispose();
        }

        private void UpdateValues()
        {
            var transforms = _tilesTransformProvider.Provide();

            _subject.OnNext(transforms);
            _single = Observable.Return(transforms);
        }
    }
}
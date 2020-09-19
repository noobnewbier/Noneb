using System;
using Common.Loaders;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using Maps.Repositories.CurrentMapTransform;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    public abstract class BoardItemsHolderLoader : ScriptableObject, ILoader
    {
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider currentMapTransformRepositoryProvider;

        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            _disposable?.Dispose();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (config, tilesTransform) = tuple;
                        InvokeLoadService(config, tilesTransform);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (config, tilesTransform) = tuple;
                        InvokeLoadService(config, tilesTransform);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(MapConfig config, Transform mapTransform)> GetDataTupleObservable()
        {
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var mapTransformObservable = currentMapTransformRepositoryProvider.Provide().GetMostRecent();

            return mapConfigObservable
                .Zip(
                    mapTransformObservable,
                    (config, mapTransform) => (config, mapTransform)
                );
        }

        private void InvokeLoadService(MapConfig config, Transform mapTransform)
        {
            var loadService = GetService();

            loadService.Load(
                mapTransform,
                config.GetMap2DActualWidth(),
                config.GetMap2DActualHeight()
            );
        }

        protected abstract ILoadBoardItemsHolderService GetService();

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
using System;
using Main.Core.Game.Common.Loaders;
using Main.Core.Game.Maps;
using Main.Core.Game.Maps.CurrentMapConfig;
using Main.Ui.Game.Maps.CurrentMapTransform;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Loaders
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
            _disposable = LoadObservable().Subscribe();
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .SelectMany(
                    tuple =>
                    {
                        var (config, tilesTransform) = tuple;
                        return InvokeLoadService(config, tilesTransform);
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

        private IObservable<Unit> InvokeLoadService(MapConfig config, Transform mapTransform)
        {
            var loadService = GetService();

            return loadService.Load(
                mapTransform,
                config
            );
        }

        protected abstract ILoadBoardItemsHolderService GetService();

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
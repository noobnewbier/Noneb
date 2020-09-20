using System;
using System.Collections.Immutable;
using Common.BoardItems;
using Common.Loaders;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems
{
    public abstract class BoardItemsLoader<TData> : ScriptableObject, ILoader where TData : BoardItemData
    {
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private IDisposable _disposable;

        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        InvokeLoadService(datas, config);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        InvokeLoadService(datas, config);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(ImmutableArray<TData> datas, MapConfig config)> GetDataTupleObservable()
        {
            var mapConfigurationObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();

            return GetDatasFromRepository(levelDataRepository)
                .Zip(mapConfigurationObservable, (datas, config) => (datas, config));
        }

        private void InvokeLoadService(ImmutableArray<TData> datas, MapConfig config)
        {
            var loadOnTileService = GetService();

            loadOnTileService.Load(
                datas,
                config.GetMap2DActualWidth(),
                config.GetMap2DActualHeight()
            );
        }

        private void OnDisable()
        {
            DisposeDisposables();
        }

        private void DisposeDisposables()
        {
            _disposable?.Dispose();
        }

        protected abstract ILoadBoardItemsService<TData> GetService();
        protected abstract IObservable<ImmutableArray<TData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository);
    }
}
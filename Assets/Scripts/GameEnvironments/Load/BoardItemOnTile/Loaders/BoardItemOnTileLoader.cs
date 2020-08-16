using System;
using System.Collections.Immutable;
using Common.BoardItems;
using Common.Holders;
using Common.Loaders;
using Common.Providers;
using Common.TagInterface;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories;
using Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public abstract class BoardItemOnTileLoader<THolder, TBoardItemOnTile, TData> : MonoBehaviour, ILoader
        where THolder : Component, IBoardItemHolder<TBoardItemOnTile>
        where TBoardItemOnTile : BoardItem, IOnTile
        where TData : IBoardItemData
    {
        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

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

        private IObservable<(ImmutableArray<TData> datas, MapConfiguration config)> GetDataTupleObservable()
        {
            var mapConfigurationObservable = mapConfigurationRepositoryProvider.Provide().Get();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();

            return GetDatasFromRepository(levelDataRepository)
                .Zip(mapConfigurationObservable, (datas, config) => (datas, config))
                .Take(1);
        }

        private void InvokeLoadService(ImmutableArray<TData> datas, MapConfiguration config)
        {
            var loadOnTileService = GetService();

            loadOnTileService.Load(
                datas,
                tilesTransformProvider,
                GetHolderProvider(),
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

        protected abstract ILoadBoardItemOnTileService<THolder, TBoardItemOnTile, TData> GetService();
        protected abstract IObservable<ImmutableArray<TData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository);
        protected abstract IGameObjectAndComponentProvider<THolder> GetHolderProvider();
    }
}
﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Common.BoardItems;
using Common.Holders;
using Common.Loaders;
using Common.Providers;
using Common.TagInterface;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using Maps.Repositories.CurrentTilesTransform;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Obsolete.BoardItemOnTile.Loaders
{
    public abstract class BoardItemOnTileLoader<THolder, TBoardItemOnTile, TData> : ScriptableObject, ILoader
        where THolder : Component, IBoardItemHolder<TBoardItemOnTile>
        where TBoardItemOnTile : BoardItem, IOnTile
        where TData : BoardItemData
    {
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [SerializeField] private CurrentTilesTransformRepositoryProvider currentTilesTransformRepositoryProvider;

        private IDisposable _disposable;

        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (datas, config, tilesTransform) = tuple;
                        InvokeLoadService(datas, config, tilesTransform);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (datas, config, tilesTransform) = tuple;
                        InvokeLoadService(datas, config, tilesTransform);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(ImmutableArray<TData> datas, MapConfig config, IList<Transform> tilesTransform)> GetDataTupleObservable()
        {
            var mapConfigurationObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var tilesTransformObservable = currentTilesTransformRepositoryProvider.Provide().GetMostRecent();

            return GetDatasFromRepository(levelDataRepository)
                .Zip(mapConfigurationObservable, tilesTransformObservable, (datas, config, tilesTransform) => (datas, config, tilesTransform));
        }

        private void InvokeLoadService(ImmutableArray<TData> datas, MapConfig config, IList<Transform> tilesTransform)
        {
            var loadOnTileService = GetService();

            loadOnTileService.Load(
                datas,
                tilesTransform,
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
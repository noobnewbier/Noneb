using System;
using System.Collections.Generic;
using Common.Loaders;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories;
using Tiles.Data;
using Tiles.Holders;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.Tiles
{
    public class MapLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;

        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .SelectMany(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        return GetLoadServiceObservable(datas, config);
                    }
                )
                .Subscribe();
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .SelectMany(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        return GetLoadServiceObservable(datas, config);
                    }
                );
        }

        private IObservable<(TileData[] datas, MapConfig config)> GetDataTupleObservable()
        {
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();

            return levelDataRepository.GetMostRecent()
                .Select(levelData => levelData.TileDatas)
                .Zip(mapConfigObservable, (datas, config) => (datas, config));
        }

        private IObservable<Unit> GetLoadServiceObservable(IList<TileData> datas, MapConfig config)
        {
            var mapLoadService = mapLoadServiceProvider.Provide();
            return mapLoadService.Load(
                datas,
                tileHolderProvider,
                rowPrefab,
                mapTransform,
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
    }
}
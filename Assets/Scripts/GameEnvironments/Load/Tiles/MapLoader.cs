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
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
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

        private IObservable<(TileData[] datas, MapConfiguration config)> GetDataTupleObservable()
        {
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = mapConfigurationRepositoryProvider.Provide().Get();

            return levelDataRepository.Get()
                .Select(levelData => levelData.TileDatas)
                .Zip(mapConfigObservable, (datas, config) => (datas, config))
                .Take(1);
        }

        private void InvokeLoadService(IList<TileData> datas, MapConfiguration config)
        {
            var mapLoadService = mapLoadServiceProvider.Provide();
            mapLoadService.Load(
                datas,
                tilesPositionProvider,
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
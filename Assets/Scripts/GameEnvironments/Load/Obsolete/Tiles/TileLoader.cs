using System;
using System.Collections.Generic;
using Common;
using Common.Loaders;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using Maps.Repositories.CurrentMapTransform;
using Tiles.Data;
using Tiles.Holders;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.Obsolete.Tiles
{
    [CreateAssetMenu(fileName = nameof(TileLoader), menuName = ProjectMenuName.Loader + nameof(TileLoader))]
    public class TileLoader : ScriptableObject, ILoader
    {
        [FormerlySerializedAs("mapLoadServiceProvider")] [SerializeField]
        private TileLoadServiceProvider tileLoadServiceProvider;

        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider mapTransformRepositoryProvider;


        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .SelectMany(
                    tuple =>
                    {
                        var (datas, config, mapTransform) = tuple;
                        return GetLoadServiceObservable(datas, config, mapTransform);
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
                        var (datas, config, mapTransform) = tuple;
                        return GetLoadServiceObservable(datas, config, mapTransform);
                    }
                );
        }

        private IObservable<(TileData[] datas, MapConfig config, Transform mapTransform)> GetDataTupleObservable()
        {
            var levelDataObservable = currentLevelDataRepositoryProvider.Provide().GetMostRecent();
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var mapTransformObservable = mapTransformRepositoryProvider.Provide().GetMostRecent();

            return levelDataObservable
                .Select(levelData => levelData.TileDatas)
                .Zip(mapConfigObservable, mapTransformObservable, (datas, config, mapTransform) => (datas, config, mapTransform));
        }

        private IObservable<Unit> GetLoadServiceObservable(IList<TileData> datas, MapConfig config, Transform mapTransform)
        {
            var loadService = tileLoadServiceProvider.Provide();
            return loadService.Load(
                datas,
                tileHolderProvider,
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
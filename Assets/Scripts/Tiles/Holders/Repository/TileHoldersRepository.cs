using System;
using System.Collections.Generic;
using System.Linq;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using Maps.Repositories.CurrentTilesTransform;
using UniRx;
using UnityEngine;

namespace Tiles.Holders.Repository
{
    public interface ITileHoldersRepository : IDisposable
    {
        IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate);
        IObservable<IReadOnlyList<TileHolder>> GetAllFlattenSingle();
        IObservable<IReadOnlyList<TileHolder>> GetAllFlattenStream();
    }

    //todo: maybe we can refactor this in to a data repository...
    public class TileHoldersRepository : ITileHoldersRepository
    {
        private IObservable<(TileHolder[,] tileHolders, MapConfig config)> _tileHoldersAndConfigSingle;
        private readonly ReplaySubject<(TileHolder[,] tileHolders, MapConfig config)> _tileHoldersAndConfigSubject;
        private readonly IDisposable _disposable;

        public TileHoldersRepository(ICurrentTilesTransformGetRepository currentTilesTransformGetRepository,
                                     ICurrentMapConfigRepository currentMapConfigRepository)
        {
            _tileHoldersAndConfigSubject = new ReplaySubject<(TileHolder[,] tileHolders, MapConfig config)>(1);
            _tileHoldersAndConfigSingle = Observable.Throw<(TileHolder[,], MapConfig)>(new InvalidOperationException("Value is not set yet"));

            _disposable = currentMapConfigRepository.GetObservableStream()
                .CombineLatest(currentTilesTransformGetRepository.GetObservableStream(), (config, tileTransforms) => (config, tileTransforms))
                .Subscribe(
                    tuple =>
                    {
                        var (config, tileTransforms) = tuple;
                        var holdersAndConfig = (GetTileHolders(tileTransforms, config), config);
                        _tileHoldersAndConfigSingle = Observable.Return(holdersAndConfig);
                        _tileHoldersAndConfigSubject.OnNext(holdersAndConfig);
                    }
                );
        }

        public IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate)
        {
            return _tileHoldersAndConfigSingle
                .Select(tuple => tuple.tileHolders[axialCoordinate.X, axialCoordinate.Z])
                .Single();
        }

        public IObservable<IReadOnlyList<TileHolder>> GetAllFlattenSingle()
        {
            return _tileHoldersAndConfigSingle.Select(
                    tuple =>
                    {
                        var (holders, config) = tuple;

                        return GetAllFlattenTileHoldersFromTileHoldersAndConfig(holders, config);
                    }
                )
                .Single();
        }

        public IObservable<IReadOnlyList<TileHolder>> GetAllFlattenStream()
        {
            return _tileHoldersAndConfigSubject
                .Where(tuple => !tuple.Equals(default))
                .Select(
                    tuple =>
                    {
                        var (holders, config) = tuple;

                        return GetAllFlattenTileHoldersFromTileHoldersAndConfig(holders, config);
                    }
                );
        }

        // private bool IsTupleValid((TileHolder[,] tileHolders, MapConfig config) tuple)
        // {
        //     return tuple.tileHolders != null && tuple.config != null;
        // }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private static IReadOnlyList<TileHolder> GetAllFlattenTileHoldersFromTileHoldersAndConfig(TileHolder[,] holders, MapConfig config)
        {
            var mapXSize = config.XSize;
            var mapZSize = config.ZSize;

            var toReturn = new TileHolder[mapXSize * mapZSize];

            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
                toReturn[i * mapXSize + j] = holders[j + i % 2 + i / 2, i];

            return toReturn;
        }

        private static TileHolder[,] GetTileHolders(IEnumerable<Transform> tilesTransforms, MapConfig mapConfig)
        {
            var mapXSize = mapConfig.XSize;
            var mapZSize = mapConfig.ZSize;
            var tiles = tilesTransforms.Select(t => t.GetComponent<TileHolder>()).ToList();

            var tileHolders = new TileHolder[mapXSize + mapZSize / 2, mapZSize];
            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
                tileHolders[j + i % 2 + i / 2, i] = tiles[i * mapXSize + j];

            return tileHolders;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Maps;
using Maps.Repositories;
using Maps.Services;
using UniRx;
using UnityEngine;

namespace Tiles.Holders.Repository
{
    public interface ITileHoldersRepository : IDisposable
    {
        IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate);
        IObservable<IReadOnlyList<TileHolder>> GetAllFlattenSingle();
    }

    public class TileHoldersRepository : ITileHoldersRepository
    {
        private IObservable<(TileHolder[,] tileHolders, MapConfig config)> _tileHoldersAndConfigSingle;
        private readonly IDisposable _disposable;

        public TileHoldersRepository(ICurrentTilesTransformGetRepository currentTilesTransformGetRepository, ICurrentMapConfigRepository currentMapConfigRepository)
        {
            _disposable = currentMapConfigRepository.GetObservableStream()
                .CombineLatest(currentTilesTransformGetRepository.GetObservableStream(), (config, tileTransforms) => (config, tileTransforms))
                .Subscribe(
                    tuple =>
                    {
                        var (config, tileTransforms) = tuple;
                        _tileHoldersAndConfigSingle = Observable.Return((GetTileHolders(tileTransforms, config), config));
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
                        var mapXSize = config.XSize;
                        var mapZSize = config.ZSize;
                        
                        var toReturn = new TileHolder[mapXSize * mapZSize];

                        for (var i = 0; i < mapZSize; i++)
                        for (var j = 0; j < mapXSize; j++)
                            toReturn[i * mapXSize + j] = holders[j + i % 2 + i / 2, i];

                        return toReturn;
                    }
                )
                .Single();
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

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using UniRx;

namespace Tiles.Holders.Repository
{
    public interface ITilesHolderService : IDisposable
    {
        IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate);
    }

    //todo: use BoardItemHoldersFetchingService's approach to avoid caching
    public class TilesHolderService : ITilesHolderService
    {
        private IObservable<(TileHolder[,] tileHolders, MapConfig config)> _tileHoldersAndConfigSingle;
        private readonly IDisposable _disposable;

        public TilesHolderService(IBoardItemHoldersFetchingService<TileHolder> tileHoldersFetchingService,
                                  ICurrentMapConfigRepository currentMapConfigRepository)
        {
            _tileHoldersAndConfigSingle = Observable.Throw<(TileHolder[,], MapConfig)>(new InvalidOperationException("Value is not set yet"));

            _disposable = currentMapConfigRepository.GetObservableStream()
                .ZipLatest(tileHoldersFetchingService.Fetch(), (config, tileTransforms) => (config, tileTransforms))
                .Subscribe(
                    tuple =>
                    {
                        var (config, tileTransforms) = tuple;
                        var holdersAndConfig = (Create2DTileHolders(tileTransforms, config), config);
                        _tileHoldersAndConfigSingle = Observable.Return(holdersAndConfig);
                    }
                );
        }

        public IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate)
        {
            return _tileHoldersAndConfigSingle
                .Select(tuple => tuple.tileHolders[axialCoordinate.X, axialCoordinate.Z])
                .Single();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }


        private static TileHolder[,] Create2DTileHolders(IReadOnlyList<TileHolder> tileHolders, MapConfig mapConfig)
        {
            var mapXSize = mapConfig.GetMap2DActualWidth();
            var mapZSize = mapConfig.GetMap2DActualHeight();

            var toReturn = new TileHolder[mapXSize + mapZSize / 2, mapZSize];
            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
                toReturn[j + i % 2 + i / 2, i] = tileHolders[i * mapXSize + j];

            return toReturn;
        }
    }
}
using System;
using System.Collections.Generic;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Core.Game.Maps;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using UniRx;

namespace Noneb.Ui.Game.Tiles
{
    public interface ITilesHolderService
    {
        IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate);
    }

    public class TilesHolderService : ITilesHolderService
    {
        private readonly IBoardItemHoldersFetchingService<TileHolder> _tileHoldersFetchingService;
        private readonly IMapConfigRepository _loadedMapConfigRepository;

        public TilesHolderService(IBoardItemHoldersFetchingService<TileHolder> tileHoldersFetchingService,
                                  IMapConfigRepository loadedMapConfigRepository)
        {
            _tileHoldersFetchingService = tileHoldersFetchingService;
            _loadedMapConfigRepository = loadedMapConfigRepository;
        }

        public IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate)
        {
            return _loadedMapConfigRepository.GetMostRecent()
                .ZipLatest(_tileHoldersFetchingService.Fetch(), (config, tileTransforms) => (config, tileTransforms))
                .Select(
                    tuple =>
                    {
                        var (config, tileTransforms) = tuple;
                        var holders = Create2DTileHolders(tileTransforms, config);

                        return holders[axialCoordinate.X, axialCoordinate.Z];
                    }
                )
                .Single();
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
using System;
using System.Collections.Generic;
using Main.Core.Game.Coordinates;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Core.Game.Maps;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using UniRx;

namespace Main.Ui.Game.Tiles
{
    public interface ITilesHolderService
    {
        IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate);
    }

    public class TilesHolderService : ITilesHolderService
    {
        private readonly IBoardItemHoldersFetchingService<TileHolder> _tileHoldersFetchingService;
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;

        public TilesHolderService(IBoardItemHoldersFetchingService<TileHolder> tileHoldersFetchingService,
                                  ICurrentMapConfigRepository currentMapConfigRepository)
        {
            _tileHoldersFetchingService = tileHoldersFetchingService;
            _currentMapConfigRepository = currentMapConfigRepository;
        }

        public IObservable<TileHolder> GetAtCoordinateSingle(Coordinate axialCoordinate)
        {
            return _currentMapConfigRepository.GetMostRecent()
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
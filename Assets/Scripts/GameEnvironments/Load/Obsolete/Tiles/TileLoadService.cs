using System;
using System.Collections.Generic;
using Common.Providers;
using GameEnvironments.Load.Obsolete.BoardItemOnTile;
using Maps.Services;
using Tiles;
using Tiles.Data;
using Tiles.Holders;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Obsolete.Tiles
{
    /// <summary>
    /// Tile cannot be load like the rest of BoardItems,
    /// as it's the only board item that needs to have it's position initialized
    /// todo: dangerously similar to <see cref="ILoadBoardItemOnTileService{THolder,TBoardItem,TBoardItemData}" />
    /// </summary>
    public interface ITileLoadService : IDisposable
    {
        IObservable<Unit> Load(IList<TileData> tileDatas,
                               IGameObjectAndComponentProvider<TileHolder> tileHolderProvider,
                               Transform mapTransform,
                               int mapXSize,
                               int mapZSize);

        IObservable<Unit> GetFinishedLoadingEventStream();
    }

    public class TileLoadService : ITileLoadService
    {
        private readonly IGetCoordinateService _getCoordinateService;
        private readonly ITilesPositionService _tilesPositionService;
        private readonly Subject<Unit> _finishedLoadingEventStream;

        public TileLoadService(IGetCoordinateService getCoordinateService, ITilesPositionService tilesPositionService)
        {
            _getCoordinateService = getCoordinateService;
            _tilesPositionService = tilesPositionService;
            _finishedLoadingEventStream = new Subject<Unit>();
        }

        public IObservable<Unit> GetFinishedLoadingEventStream()
        {
            return _finishedLoadingEventStream;
        }

        public IObservable<Unit> Load(IList<TileData> tileDatas,
                                      IGameObjectAndComponentProvider<TileHolder> tileHolderProvider,
                                      Transform mapTransform,
                                      int mapXSize,
                                      int mapZSize)
        {
            return _tilesPositionService.GetMostRecent(mapTransform.position.y)
                .Select(
                    positions =>
                    {
                        for (var i = 0; i < mapZSize; i++)
                        for (var j = 0; j < mapXSize; j++)
                        {
                            var (component, gameObject) = tileHolderProvider.Provide(mapTransform, false);
                            var index = i * mapXSize + j;

                            gameObject.transform.position = positions[index];
                            component.Initialize(
                                new Tile(tileDatas[index], _getCoordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i))
                            );
                        }

                        _finishedLoadingEventStream.OnNext(Unit.Default);
                        return Unit.Default;
                    }
                );
        }

        public void Dispose()
        {
            _finishedLoadingEventStream?.Dispose();
        }
    }
}
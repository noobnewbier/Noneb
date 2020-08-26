using System;
using System.Collections.Generic;
using Common.Providers;
using GameEnvironments.Load.BoardItemOnTile;
using Maps.Services;
using Tiles;
using Tiles.Data;
using Tiles.Holders;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEnvironments.Load.Tiles
{
    /// <summary>
    /// Tile cannot be load like the rest of BoardItems,
    /// as it's the only board item that needs to have it's position initialized
    /// todo: dangerously similar to <see cref="ILoadBoardItemOnTileService{THolder,TBoardItem,TBoardItemData}" />
    /// </summary>
    public interface IMapLoadService : IDisposable
    {
        IObservable<Unit> Load(IList<TileData> tileDatas,
                               IGameObjectAndComponentProvider<TileHolder> tileHolderProvider,
                               GameObject rowPrefab,
                               Transform mapTransform,
                               int mapXSize,
                               int mapZSize);

        IObservable<Unit> GetFinishedLoadingEventStream();
    }

    public class MapLoadService : IMapLoadService
    {
        private readonly IGetCoordinateService _getCoordinateService;
        private readonly ITilesPositionService _tilesPositionService;
        private readonly Subject<Unit> _finishedLoadingEventStream;

        public MapLoadService(IGetCoordinateService getCoordinateService, ITilesPositionService tilesPositionService)
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
                                      GameObject rowPrefab,
                                      Transform mapTransform,
                                      int mapXSize,
                                      int mapZSize)
        {

            return _tilesPositionService.GetMostRecent(mapTransform.position.y)
                .Select(
                    positions =>
                    {
                        for (var i = 0; i < mapZSize; i++)
                        {
                            var row = Object.Instantiate(rowPrefab).transform;
                            row.parent = mapTransform;
                            for (var j = 0; j < mapXSize; j++)
                            {
                                var tileAndGo = tileHolderProvider.Provide(row, false);
                                var index = i * mapXSize + j;

                                tileAndGo.GameObject.transform.position = positions[index];
                                tileAndGo.Component.Initialize(
                                    new Tile(tileDatas[index], _getCoordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i))
                                );
                            }
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
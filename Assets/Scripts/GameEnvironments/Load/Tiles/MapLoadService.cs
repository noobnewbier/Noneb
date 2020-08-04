using System.Collections.Generic;
using Common.Providers;
using GameEnvironments.Load.BoardItemOnTile;
using Maps.Services;
using Tiles;
using Tiles.Data;
using Tiles.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Tiles
{
    /// <summary>
    /// Tile cannot be load like the rest of BoardItems,
    /// as it's the only board item that needs to have it's position initialized
    ///
    /// todo: dangerously similar to <see cref="ILoadBoardItemOnTileService{THolder,TBoardItem,TBoardItemData}"/>
    /// </summary>
    public interface IMapLoadService
    {
        void Load(IList<TileData> tileDatas,
                  IObjectProvider<IList<Vector3>> tilesPositionProvider,
                  IGameObjectAndComponentProvider<TileHolder> tileHolderProvider,
                  GameObject rowPrefab,
                  Transform baseTransform,
                  int mapXSize,
                  int mapZSize);
    }

    public class MapLoadService : IMapLoadService
    {
        private readonly IGetCoordinateService _getCoordinateService;

        public MapLoadService(IGetCoordinateService getCoordinateService)
        {
            _getCoordinateService = getCoordinateService;
        }

        public void Load(IList<TileData> tileDatas,
                         IObjectProvider<IList<Vector3>> tilesPositionProvider,
                         IGameObjectAndComponentProvider<TileHolder> tileHolderProvider,
                         GameObject rowPrefab,
                         Transform baseTransform,
                         int mapXSize,
                         int mapZSize)
        {
            var positions = tilesPositionProvider.Provide();

            for (var i = 0; i < mapZSize; i++)
            {
                var row = Object.Instantiate(rowPrefab).transform;
                row.parent = baseTransform;
                for (var j = 0; j < mapXSize; j++)
                {
                    var tileAndGo = tileHolderProvider.Provide(row, false);
                    var index = i * mapXSize + j;

                    tileAndGo.GameObject.transform.position = positions[index];
                    tileAndGo.Component.Initialize(new Tile(tileDatas[index], _getCoordinateService.GetAxialCoordinate(j, i)));
                }
            }
        }
    }
}
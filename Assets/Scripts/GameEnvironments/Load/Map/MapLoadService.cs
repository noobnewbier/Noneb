using System.Collections.Generic;
using Common;
using Common.Providers;
using Maps.Services;
using Tiles;
using Tiles.Data;
using Tiles.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Map
{
    public interface IMapLoadService
    {
        void Load(IList<TileData> tileDatas,
                  IObjectProvider<IList<Vector3>> tilesPositionProvider,
                  IObjectProvider<GameObjectAndComponent<TileHolder>> tileHolderProvider,
                  GameObject rowPrefab,
                  Transform baseTransform,
                  Vector3 upAxis,
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
                         IObjectProvider<GameObjectAndComponent<TileHolder>> tileHolderProvider,
                         GameObject rowPrefab,
                         Transform baseTransform,
                         Vector3 upAxis,
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
                    var tileAndGo = tileHolderProvider.Provide();
                    var index = i * mapXSize + j;

                    //set gameObject's position
                    var gameObjectTransform = tileAndGo.GameObject.transform;
                    gameObjectTransform.parent = row;
                    gameObjectTransform.rotation *= Quaternion.AngleAxis(30f, upAxis);
                    gameObjectTransform.position = positions[index];

                    Debug.Log(tileAndGo.Component);
                    //set tile's data
                    tileAndGo.Component.Initialize(new Tile(_getCoordinateService.GetAxialCoordinate(j, i), tileDatas[index]));
                }
            }
        }
    }
}
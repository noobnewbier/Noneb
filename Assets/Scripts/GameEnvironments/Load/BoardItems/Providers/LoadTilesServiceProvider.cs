using Common.Factories;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps;
using Maps.Services;
using Tiles;
using Tiles.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadTilesServiceProvider), menuName = MenuName.ScriptableService + "LoadTilesService")]
    public class LoadTilesServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Tile, TileData>>
    {
        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;
        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsService<Tile, TileData> _cache;

        public override LoadBoardItemsService<Tile, TileData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Tile, TileData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<TileData, Coordinate, Tile>
                    ((data, coordinate) => new Tile(data, coordinate)),
                tilesRepositoryProvider.Provide()
            ));
        }
    }
}
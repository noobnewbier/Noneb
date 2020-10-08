using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadTilesServiceProvider), menuName = MenuName.ScriptableService + "LoadTilesService")]
    public class LoadTilesServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Tile, TileData>>
    {
        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

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
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.GameState.BoardItems.Providers;
using Main.Core.Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadTilesServiceProvider), menuName = MenuName.ScriptableService + "LoadTilesService")]
    public class LoadTilesServiceProvider : ScriptableObject, IObjectProvider<ILoadBoardItemsService<TileData>>
    {
        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemsService<TileData> _cache;

        public ILoadBoardItemsService<TileData> Provide()
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
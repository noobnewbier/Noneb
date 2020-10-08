using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Tiles;
using Main.Ui.Game.Maps.TilesPosition;
using Main.Ui.Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadTilesHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadTilesHolderService")]
    public class LoadTilesHolderServiceProvider : ScriptableObject, IObjectProvider<LoadBoardItemsHolderService<TileHolder, Tile>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;

        [FormerlySerializedAs("tileHolderProvider")] [SerializeField]
        private TileHolderFactory tileHolderFactory;

        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<TileHolder, Tile> _cache;

        public LoadBoardItemsHolderService<TileHolder, Tile> Provide() =>
            _cache ?? (_cache = new LoadBoardItemsHolderService<TileHolder, Tile>(
                tilesPositionServiceProvider.Provide(),
                tilesRepositoryProvider.Provide(),
                tileHolderFactory,
                coordinateServiceProvider.Provide()
            ));
    }
}
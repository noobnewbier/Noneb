﻿using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Services;
using Tiles;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadTilesHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadTilesHolderService")]
    public class LoadTilesHolderServiceProvider : ScriptableObjectProvider<LoadBoardItemsHolderService<TileHolder, Tile>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;


        private LoadBoardItemsHolderService<TileHolder, Tile> _cache;

        public override LoadBoardItemsHolderService<TileHolder, Tile> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsHolderService<TileHolder, Tile>(
                tilesPositionServiceProvider.Provide(),
                tilesRepositoryProvider.Provide(),
                tileHolderProvider
            ));
        }
    }
}
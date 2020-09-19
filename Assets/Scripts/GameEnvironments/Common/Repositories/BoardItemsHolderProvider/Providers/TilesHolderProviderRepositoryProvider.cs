using System;
using Common.Providers;
using Tiles;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider.Providers
{
    [CreateAssetMenu(
        fileName = nameof(TilesHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "TilesHolderProviderRepository"
    )]
    public class TilesHolderProviderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderProviderRepository<TilesHolderProvider, TileHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<TilesHolderProvider, TileHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<TilesHolderProvider, TileHolder>>(
                () => new BoardItemsHolderProviderRepository<TilesHolderProvider, TileHolder>()
            );

        public override BoardItemsHolderProviderRepository<TilesHolderProvider, TileHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}
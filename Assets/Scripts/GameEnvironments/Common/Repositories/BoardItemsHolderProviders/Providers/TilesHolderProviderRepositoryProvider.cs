using System;
using Common.BoardItems;
using Common.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(TilesHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "TilesHolderProviderRepository"
    )]
    public class TilesHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<BoardItemsHolderProvider<TileHolder>, TileHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<TileHolder>, TileHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<TileHolder>, TileHolder>>(
                () => new BoardItemsHolderProviderRepository<BoardItemsHolderProvider<TileHolder>, TileHolder>()
            );

        public override BoardItemsHolderProviderRepository<BoardItemsHolderProvider<TileHolder>, TileHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}
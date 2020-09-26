using System;
using Common.BoardItems;
using Common.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(TilesHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "TilesHolderFetcherRepository"
    )]
    public class TilesHolderFetcherRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>()
            );

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}
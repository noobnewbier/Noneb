using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers
{
    [CreateAssetMenu(
        fileName = nameof(TilesHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "TilesHolderFetcherRepository"
    )]
    public class TilesHolderFetcherRepositoryProvider : ScriptableObject,
                                                        IObjectProvider<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder>()
            );

        public BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder> Provide() => _lazyInstance.Value;
    }
}
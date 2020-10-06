using System;
using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers
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

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<TileHolder>, TileHolder> Provide() => _lazyInstance.Value;
    }
}
using System;
using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.UiState.BoardItemsFetcherRepository.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "StrongholdsHolderFetcherRepository"
    )]
    public class StrongholdsHolderFetcherRepositoryProvider : ScriptableObject,
                                                              IObjectProvider<BoardItemsHolderFetcherRepository<
                                                                  BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>()
            );

        public BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder> Provide() =>
            _lazyInstance.Value;
    }
}
using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "StrongholdsHolderFetcherRepository"
    )]
    public class StrongholdsHolderFetcherRepositoryProvider : ScriptableObject,
                                                              IObjectProvider<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>()
            );

        public BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder> Provide() =>
            _lazyInstance.Value;
    }
}
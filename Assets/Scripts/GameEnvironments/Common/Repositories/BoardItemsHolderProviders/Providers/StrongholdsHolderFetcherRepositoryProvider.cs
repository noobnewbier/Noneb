using System;
using Common.BoardItems;
using Common.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "StrongholdsHolderFetcherRepository"
    )]
    public class StrongholdsHolderFetcherRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder>()
            );

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<StrongholdHolder>, StrongholdHolder> Provide() =>
            _lazyInstance.Value;
    }
}
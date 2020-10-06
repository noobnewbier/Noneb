using System;
using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers
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
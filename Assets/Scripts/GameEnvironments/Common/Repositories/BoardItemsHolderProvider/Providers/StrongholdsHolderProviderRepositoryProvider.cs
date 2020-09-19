using System;
using Common.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "StrongholdsHolderProviderRepository"
    )]
    public class StrongholdsHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<StrongholdsHolderProvider, StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<StrongholdsHolderProvider, StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<StrongholdsHolderProvider, StrongholdHolder>>(
                () => new BoardItemsHolderProviderRepository<StrongholdsHolderProvider, StrongholdHolder>()
            );

        public override BoardItemsHolderProviderRepository<StrongholdsHolderProvider, StrongholdHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}
using System;
using Common.BoardItems;
using Common.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "StrongholdsHolderProviderRepository"
    )]
    public class StrongholdsHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<BoardItemsHolderProvider<StrongholdHolder>, StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<StrongholdHolder>, StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<StrongholdHolder>, StrongholdHolder>>(
                () => new BoardItemsHolderProviderRepository<BoardItemsHolderProvider<StrongholdHolder>, StrongholdHolder>()
            );

        public override BoardItemsHolderProviderRepository<BoardItemsHolderProvider<StrongholdHolder>, StrongholdHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}
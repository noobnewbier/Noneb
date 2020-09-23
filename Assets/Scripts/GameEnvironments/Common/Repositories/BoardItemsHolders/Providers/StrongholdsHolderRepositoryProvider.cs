using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsHolderRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsHolderRepository")]
    public class StrongholdsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<StrongholdHolder>>
    {
        [SerializeField] private StrongholdsHolderProviderRepositoryProvider providerRepositoryProvider;
        [SerializeField] private LoadStrongholdsHolderServiceProvider loadServiceProvider;


        private BoardItemsHolderGetRepository<StrongholdHolder> _cache;

        public override BoardItemsHolderGetRepository<StrongholdHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<StrongholdHolder>(
                providerRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
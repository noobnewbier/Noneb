using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsHolderRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsHolderRepository")]
    public class StrongholdsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<StrongholdHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField] private StrongholdsHolderFetcherRepositoryProvider fetcherRepositoryProvider;
        [SerializeField] private LoadStrongholdsHolderServiceProvider loadServiceProvider;


        private BoardItemsHolderGetRepository<StrongholdHolder> _cache;

        public override BoardItemsHolderGetRepository<StrongholdHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<StrongholdHolder>(
                fetcherRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
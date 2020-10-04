using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdHoldersFetchingServiceRepositoryProvider),
        menuName = MenuName.ScriptableService + "StrongholdsHoldersFetchingService"
    )]
    public class StrongholdHoldersFetchingServiceRepositoryProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<StrongholdHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private StrongholdsHolderFetcherRepositoryProvider fetcherRepositoryProvider;


        private BoardItemHoldersFetchingService<StrongholdHolder> _cache;

        public override BoardItemHoldersFetchingService<StrongholdHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemHoldersFetchingService<StrongholdHolder>(
                fetcherRepositoryProvider.Provide()
            ));
        }
    }
}
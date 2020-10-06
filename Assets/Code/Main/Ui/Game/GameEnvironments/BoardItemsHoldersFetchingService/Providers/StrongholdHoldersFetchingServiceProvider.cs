using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdHoldersFetchingServiceProvider),
        menuName = MenuName.ScriptableService + "StrongholdsHoldersFetchingService"
    )]
    public class StrongholdHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<StrongholdHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private StrongholdsHolderFetcherRepositoryProvider fetcherRepositoryProvider;


        private BoardItemHoldersFetchingService<StrongholdHolder> _cache;

        public override BoardItemHoldersFetchingService<StrongholdHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<StrongholdHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsHoldersFetchingServiceProvider), menuName = MenuName.ScriptableService + "ConstructsFetchingServiceRepository")]
    public class ConstructsHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<ConstructHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField] private ConstructsHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private BoardItemHoldersFetchingService<ConstructHolder> _cache;

        public override BoardItemHoldersFetchingService<ConstructHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemHoldersFetchingService<ConstructHolder>(
                fetcherRepositoryProvider.Provide()
            ));
        }
    }
}
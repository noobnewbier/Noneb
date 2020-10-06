using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHoldersFetchingServiceProvider),
        menuName = MenuName.ScriptableService + "ConstructsHoldersFetchingService"
    )]
    public class ConstructsHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<ConstructHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private ConstructsHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private BoardItemHoldersFetchingService<ConstructHolder> _cache;

        public override BoardItemHoldersFetchingService<ConstructHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<ConstructHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}
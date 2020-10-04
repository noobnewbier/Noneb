using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Units.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitHoldersFetchingServiceProvider), menuName = MenuName.ScriptableService + "UnitsHoldersFetchingService")]
    public class UnitHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<UnitHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private UnitsHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private BoardItemHoldersFetchingService<UnitHolder> _cache;

        public override BoardItemHoldersFetchingService<UnitHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<UnitHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}
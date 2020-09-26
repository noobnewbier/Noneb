using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Units.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsHolderRepositoryProvider), menuName = MenuName.ScriptableRepository + "UnitsHolderRepository")]
    public class UnitsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<UnitHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField] private UnitsHolderFetcherRepositoryProvider fetcherRepositoryProvider;
        [SerializeField] private LoadUnitsHolderServiceProvider loadServiceProvider;

        private BoardItemsHolderGetRepository<UnitHolder> _cache;

        public override BoardItemsHolderGetRepository<UnitHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<UnitHolder>(
                fetcherRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
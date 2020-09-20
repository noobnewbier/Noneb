using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsHolderRepositoryProvider), menuName = MenuName.Providers + "UnitsHolderRepository")]
    public class UnitsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<UnitHolder>>
    {
        [SerializeField] private UnitsHolderProviderRepositoryProvider providerRepositoryProvider;
        [SerializeField] private LoadUnitsHolderServiceProvider loadServiceProvider;

        private BoardItemsHolderGetRepository<UnitHolder> _cache;

        public override BoardItemsHolderGetRepository<UnitHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<UnitHolder>(
                providerRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
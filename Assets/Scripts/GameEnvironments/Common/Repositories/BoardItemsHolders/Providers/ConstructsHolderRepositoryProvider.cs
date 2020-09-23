using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsHolderRepositoryProvider), menuName = MenuName.ScriptableRepository + "ConstructsHolderRepository")]
    public class ConstructsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<ConstructHolder>>
    {
        [SerializeField] private ConstructsHolderProviderRepositoryProvider providerRepositoryProvider;
        [SerializeField] private LoadConstructsHolderServiceProvider loadServiceProvider;

        private BoardItemsHolderGetRepository<ConstructHolder> _cache;

        public override BoardItemsHolderGetRepository<ConstructHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<ConstructHolder>(
                providerRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
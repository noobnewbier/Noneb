using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesHolderRepositoryProvider), menuName = MenuName.Providers + "TilesHolderRepository")]
    public class TilesHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<TileHolder>>
    {
        [SerializeField] private TilesHolderProviderRepositoryProvider providerRepositoryProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadServiceProvider;
        
        
        private BoardItemsHolderGetRepository<TileHolder> _cache;

        public override BoardItemsHolderGetRepository<TileHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<TileHolder>(providerRepositoryProvider.Provide(), loadServiceProvider.Provide()));
        }
    }
}
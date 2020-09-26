using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesHolderRepositoryProvider), menuName = MenuName.ScriptableRepository + "TilesHolderRepository")]
    public class TilesHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderGetRepository<TileHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField] private TilesHolderFetcherRepositoryProvider fetcherRepositoryProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadServiceProvider;


        private BoardItemsHolderGetRepository<TileHolder> _cache;

        public override BoardItemsHolderGetRepository<TileHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemsHolderGetRepository<TileHolder>(
                fetcherRepositoryProvider.Provide(),
                loadServiceProvider.Provide()
            ));
        }
    }
}
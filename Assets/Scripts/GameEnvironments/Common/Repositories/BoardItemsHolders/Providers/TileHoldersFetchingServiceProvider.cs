using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using GameEnvironments.Load.Holders.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders.Providers
{
    [CreateAssetMenu(fileName = nameof(TileHoldersFetchingServiceProvider), menuName = MenuName.ScriptableService + "TileHoldersFetchingService")]
    public class TileHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<TileHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField] private TilesHolderFetcherRepositoryProvider fetcherRepositoryProvider;


        private BoardItemHoldersFetchingService<TileHolder> _cache;

        public override BoardItemHoldersFetchingService<TileHolder> Provide()
        {
            return _cache ?? (_cache = new BoardItemHoldersFetchingService<TileHolder>(
                fetcherRepositoryProvider.Provide()
            ));
        }
    }
}
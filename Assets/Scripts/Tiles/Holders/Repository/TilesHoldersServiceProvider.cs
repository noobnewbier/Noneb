using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using Maps.Repositories.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Tiles.Holders.Repository
{
    [CreateAssetMenu(fileName = nameof(TilesHoldersServiceProvider), menuName = MenuName.ScriptableRepository + nameof(TilesHolderService))]
    public class TilesHoldersServiceProvider : ScriptableObjectProvider<ITilesHolderService>
    {
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [FormerlySerializedAs("tileHolderssFetchingServiceProvider")] [FormerlySerializedAs("tilesHolderRepositoryProvider")] [SerializeField] private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;


        private ITilesHolderService _cache;

        private void OnDisable()
        {
            _cache?.Dispose();
            _cache = null;
        }

        public override ITilesHolderService Provide()
        {
            return _cache ?? (_cache = new TilesHolderService(
                tileHoldersFetchingServiceProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide()
            ));
        }
    }
}
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using Maps.Repositories.CurrentMapConfig;
using UnityEngine;
using UnityUtils.Constants;

namespace Tiles.Holders.Repository
{
    [CreateAssetMenu(fileName = nameof(TilesHoldersServiceProvider), menuName = MenuName.ScriptableRepository + nameof(TilesHolderService))]
    public class TilesHoldersServiceProvider : ScriptableObjectProvider<ITilesHolderService>
    {
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [SerializeField] private TilesHolderRepositoryProvider tilesHolderRepositoryProvider;


        private ITilesHolderService _cache;

        private void OnDisable()
        {
            _cache?.Dispose();
            _cache = null;
        }

        public override ITilesHolderService Provide()
        {
            return _cache ?? (_cache = new TilesHolderService(
                tilesHolderRepositoryProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide()
            ));
        }
    }
}
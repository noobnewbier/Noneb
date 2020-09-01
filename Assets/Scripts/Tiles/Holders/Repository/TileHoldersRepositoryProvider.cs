using Common.Providers;
using Maps.Repositories;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace Tiles.Holders.Repository
{
    [CreateAssetMenu(fileName = nameof(TileHoldersRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(TileHoldersRepository))]
    public class TileHoldersRepositoryProvider : ScriptableObjectProvider<ITileHoldersRepository>
    {
        [SerializeField] private CurrentTilesTransformRepositoryProvider tilesTransformRepositoryProvider;
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private ITileHoldersRepository _cache;

        private void OnDisable()
        {
            _cache?.Dispose();
            _cache = null;
        }

        public override ITileHoldersRepository Provide()
        {
            return _cache ?? (_cache = new TileHoldersRepository(
                tilesTransformRepositoryProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide()
            ));
        }
    }
}
using Common.Providers;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Obsolete.Tiles
{
    [CreateAssetMenu(fileName = nameof(TileLoadServiceProvider), menuName = MenuName.ScriptableService + "MapLoaderService")]
    public class TileLoadServiceProvider : ScriptableObjectProvider<ITileLoadService>
    {
        [SerializeField] private GetCoordinateServiceProvider getCoordinateServiceProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;

        private ITileLoadService _cache;

        public override ITileLoadService Provide()
        {
            return _cache ?? (_cache = new TileLoadService(getCoordinateServiceProvider.Provide(), tilesPositionServiceProvider.Provide()));
        }

        private void OnDisable()
        {
            _cache?.Dispose();
        }
    }
}
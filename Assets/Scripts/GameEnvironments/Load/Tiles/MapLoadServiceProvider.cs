using System;
using Common.Providers;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Tiles
{
    [CreateAssetMenu(fileName = nameof(MapLoadServiceProvider), menuName = MenuName.ScriptableService + "MapLoaderService")]
    public class MapLoadServiceProvider : ScriptableObjectProvider<IMapLoadService>
    {
        [SerializeField] private GetCoordinateServiceProvider getCoordinateServiceProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;

        private IMapLoadService _cache;

        public override IMapLoadService Provide()
        {
            return _cache ?? (_cache = new MapLoadService(getCoordinateServiceProvider.Provide(), tilesPositionServiceProvider.Provide()));
        }

        private void OnDisable()
        {
            _cache?.Dispose();
        }
    }
}
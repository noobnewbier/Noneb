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

        private IMapLoadService _service;

        private void OnEnable()
        {
            _service = new MapLoadService(getCoordinateServiceProvider.Provide());
        }

        public override IMapLoadService Provide()
        {
            return _service;
        }
    }
}
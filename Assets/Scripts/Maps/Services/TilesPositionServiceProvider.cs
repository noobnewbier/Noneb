using Common.Providers;
using Maps.Repositories;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;
using WorldConfigurations.Repositories;

namespace Maps.Services
{
    [CreateAssetMenu(fileName = nameof(TilesPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(TilesPositionService))]
    public class TilesPositionServiceProvider : ScriptableObjectProvider<ITilesPositionService>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        private ITilesPositionService _cache;

        public override ITilesPositionService Provide()
        {
            return _cache ?? (_cache = new TilesPositionService(
                currentMapConfigRepositoryProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide()
            ));
        }
    }
}
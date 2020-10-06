using Main.Core.Game.Common.Providers;
using Main.Core.Game.Maps.CurrentMapConfig;
using Main.Core.Game.WorldConfigurations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.Maps.TilesPosition
{
    [CreateAssetMenu(fileName = nameof(TilesPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(TilesPositionService))]
    public class TilesPositionServiceProvider : ScriptableObjectProvider<ITilesPositionService>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        private ITilesPositionService _cache;

        public override ITilesPositionService Provide() =>
            _cache ?? (_cache = new TilesPositionService(
                currentMapConfigRepositoryProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide()
            ));
    }
}
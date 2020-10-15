using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Core.Game.GameState.CurrentWorldConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Maps.TilesPosition
{
    [CreateAssetMenu(fileName = nameof(TilesPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(TilesPositionService))]
    public class TilesPositionServiceProvider : ScriptableObject, IObjectProvider<ITilesPositionService>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        private ITilesPositionService _cache;

        public ITilesPositionService Provide() =>
            _cache ?? (_cache = new TilesPositionService(
                currentMapConfigRepositoryProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide()
            ));
    }
}
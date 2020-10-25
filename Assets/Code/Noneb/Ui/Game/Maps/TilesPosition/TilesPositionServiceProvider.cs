using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.GameState.WorldConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Maps.TilesPosition
{
    [CreateAssetMenu(fileName = nameof(TilesPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(TilesPositionService))]
    public class TilesPositionServiceProvider : ScriptableObject, IObjectProvider<ITilesPositionService>
    {
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private SelectedMapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        [FormerlySerializedAs("currentWorldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private SelectedWorldConfigRepositoryProvider selectedWorldConfigRepositoryProvider;

        private ITilesPositionService _cache;

        public ITilesPositionService Provide() =>
            _cache ?? (_cache = new TilesPositionService(
                selectedMapConfigRepositoryProvider.Provide(),
                selectedWorldConfigRepositoryProvider.Provide()
            ));
    }
}
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.GameState.WorldConfig;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Maps.TilesPosition
{
    [CreateAssetMenu(fileName = nameof(TilesPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(TilesPositionService))]
    public class TilesPositionServiceProvider : ScriptableObject, IObjectProvider<ITilesPositionService>
    {
        [SerializeField] private MapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        [SerializeField] private WorldConfigRepositoryProvider selectedWorldConfigRepositoryProvider;

        private ITilesPositionService _cache;

        public ITilesPositionService Provide() =>
            _cache ?? (_cache = new TilesPositionService(
                selectedMapConfigRepositoryProvider.Provide(),
                selectedWorldConfigRepositoryProvider.Provide()
            ));
    }
}
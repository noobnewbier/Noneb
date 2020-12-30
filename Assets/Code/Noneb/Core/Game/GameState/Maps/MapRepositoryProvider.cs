using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.BoardItems.Providers;
using Noneb.Core.Game.GameState.MapConfigs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.Maps
{
    [CreateAssetMenu(fileName = nameof(MapRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(MapGetService))]
    public class MapRepositoryProvider : ScriptableObject, IObjectProvider<IMapGetService>
    {
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private MapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        private IMapGetService _cache;

        public IMapGetService Provide() => _cache ?? (_cache = new MapGetService(
            selectedMapConfigRepositoryProvider.Provide(),
            tilesRepositoryProvider.Provide(),
            unitsRepositoryProvider.Provide(),
            constructsRepositoryProvider.Provide(),
            strongholdsRepositoryProvider.Provide()
        ));
    }
}
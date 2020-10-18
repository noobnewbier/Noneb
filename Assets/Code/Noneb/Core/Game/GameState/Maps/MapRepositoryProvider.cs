using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.BoardItems.Providers;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.Maps
{
    [CreateAssetMenu(fileName = nameof(MapRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(MapRepository))]
    public class MapRepositoryProvider : ScriptableObject, IObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        private IMapRepository _cache;

        public IMapRepository Provide() => _cache ?? (_cache = new MapRepository(
            currentMapConfigRepositoryProvider.Provide(),
            tilesRepositoryProvider.Provide(),
            unitsRepositoryProvider.Provide(),
            constructsRepositoryProvider.Provide(),
            strongholdsRepositoryProvider.Provide()
        ));
    }
}
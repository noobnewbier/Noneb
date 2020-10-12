using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameState.BoardItems.Providers;
using Main.Core.Game.GameState.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.Maps
{
    [CreateAssetMenu(fileName = nameof(MapRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(MapRepository))]
    public class MapRepositoryProvider : ScriptableObject, IObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;

        private IMapRepository _cache;

        public IMapRepository Provide() => _cache ?? (_cache = new MapRepository(
            currentMapConfigRepositoryProvider.Provide(),
            tilesRepositoryProvider.Provide()
        ));
    }
}
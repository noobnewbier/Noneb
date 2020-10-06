using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.Maps.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Core.Game.Maps
{
    [CreateAssetMenu(fileName = nameof(MapRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(MapRepository))]
    public class MapRepositoryProvider : ScriptableObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;

        private IMapRepository _cache;

        public override IMapRepository Provide() => _cache ?? (_cache = new MapRepository(
            currentMapConfigRepositoryProvider.Provide(),
            tilesRepositoryProvider.Provide()
        ));
    }
}
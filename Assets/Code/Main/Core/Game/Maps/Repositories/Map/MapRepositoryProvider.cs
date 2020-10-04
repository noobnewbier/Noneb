using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Repositories.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Maps.Repositories.Map
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
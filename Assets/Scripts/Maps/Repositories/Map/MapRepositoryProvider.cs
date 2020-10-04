using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Repositories.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps.Repositories.Map
{
    //todo: can be a scriptable
    public class MapRepositoryProvider : MonoObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesRepositoryProvider tilesRepositoryProvider;
        
        private IMapRepository _cache;

        public override IMapRepository Provide()
        {
            return _cache ?? (_cache = new MapRepository(currentMapConfigRepositoryProvider.Provide(), tilesRepositoryProvider.Provide()));
        }
    }
}
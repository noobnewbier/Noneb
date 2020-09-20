using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolder.Providers;
using Maps.Repositories.CurrentMapConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps.Repositories.Map
{
    public class MapRepositoryProvider : MonoObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesHolderRepositoryProvider tilesHolderRepositoryProvider;
        

        private IMapRepository _cache;

        public override IMapRepository Provide()
        {
            return _cache ?? (_cache = new MapRepository(currentMapConfigRepositoryProvider.Provide(), tilesHolderRepositoryProvider.Provide()));
        }
    }
}
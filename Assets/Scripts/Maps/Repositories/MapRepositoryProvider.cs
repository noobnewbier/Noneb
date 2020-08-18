using Common.Providers;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps.Repositories
{
    public class MapRepositoryProvider : MonoObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [SerializeField] private TileHolderRepositoryProvider tileHolderRepositoryProvider;
        
        private IMapRepository _cache;
        
        public override IMapRepository Provide()
        {
            return _cache ?? (_cache = new MapRepository(currentMapConfigRepositoryProvider.Provide(),tileHolderRepositoryProvider.Provide()));
        }
    }
}

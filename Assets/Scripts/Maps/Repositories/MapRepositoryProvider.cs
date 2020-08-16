using Common.Providers;
using Tiles.Holders.Repository;
using UnityEngine;

namespace Maps.Repositories
{
    public class MapRepositoryProvider : MonoObjectProvider<IMapRepository>
    {
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private TileHolderRepositoryProvider tileHolderRepositoryProvider;
        
        private IMapRepository _cache;
        
        public override IMapRepository Provide()
        {
            return _cache ?? (_cache = new MapRepository(mapConfigurationRepositoryProvider.Provide(),tileHolderRepositoryProvider.Provide()));
        }
    }
}

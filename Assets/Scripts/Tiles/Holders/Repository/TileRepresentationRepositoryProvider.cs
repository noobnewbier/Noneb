using System.Linq;
using Common.Providers;
using Maps;
using UnityEngine;

namespace Tiles.Holders.Repository
{
    public class TileRepresentationRepositoryProvider : MonoObjectProvider<ITileHoldersRepository>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfiguration configuration;
        
        private ITileHoldersRepository _repository;
        
        public override ITileHoldersRepository Provide()
        {
            if (_repository == null)
            {
                var representations = tilesTransformProvider.Provide().Select(t => t.GetComponent<TileHolder>()).ToList();
                _repository = new TileHoldersRepository(representations, configuration.XSize, configuration.ZSize);                
            }

            return _repository;
        }
    }
}
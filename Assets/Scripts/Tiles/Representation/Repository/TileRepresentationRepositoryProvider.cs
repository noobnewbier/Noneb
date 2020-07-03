using System.Linq;
using Common;
using Maps;
using UnityEngine;

namespace Tiles.Representation.Repository
{
    public class TileRepresentationRepositoryProvider : MonoObjectProvider<ITileRepresentationRepository>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfiguration configuration;
        
        private ITileRepresentationRepository _repository;
        
        public override ITileRepresentationRepository Provide()
        {
            if (_repository == null)
            {
                var representations = tilesTransformProvider.Provide().Select(t => t.GetComponent<TileRepresentation>()).ToList();
                _repository = new TileRepresentationRepository(representations, configuration.XSize, configuration.ZSize);                
            }

            return _repository;
        }
    }
}
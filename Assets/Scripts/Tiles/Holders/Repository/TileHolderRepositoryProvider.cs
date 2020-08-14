using System.Linq;
using Common.Providers;
using Maps;
using Maps.Repositories;
using UnityEngine;

namespace Tiles.Holders.Repository
{
    public class TileHolderRepositoryProvider : MonoObjectProvider<ITileHoldersRepository>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        
        private ITileHoldersRepository _repository;
        
        public override ITileHoldersRepository Provide()
        {
            if (_repository == null)
            {
                var mapConfig = mapConfigurationRepositoryProvider.Provide().Get();
                var representations = tilesTransformProvider.Provide().Select(t => t.GetComponent<TileHolder>()).ToList();
                _repository = new TileHoldersRepository(representations, mapConfig.XSize, mapConfig.ZSize);                
            }

            return _repository;
        }
    }
}
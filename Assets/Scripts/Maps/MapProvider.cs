using System.Linq;
using Common;
using Tiles;
using Tiles.Representation.Repository;
using UnityEngine;

namespace Maps
{
    public class MapProvider : MonoObjectProvider<Map>
    {
        private Map _map;
        [SerializeField] private MapConfiguration mapConfiguration;
        [SerializeField] private TileRepresentationRepositoryProvider tileRepresentationRepositoryProvider;

        public override Map Provide()
        {
            if (_map == null)
            {
                var tileRepresentationRepository = tileRepresentationRepositoryProvider.Provide();
                _map = new Map(tileRepresentationRepository.GetAllFlatten().Select(t => t.Tile).ToList(), mapConfiguration.XSize, mapConfiguration.ZSize);
            }

            return _map;
        }
    }
}
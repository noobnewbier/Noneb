using System.Linq;
using Common;
using Tiles;
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
                var tiles = tileRepresentationRepositoryProvider.Provide();
                _map = new Map(tiles.GetAllFlatten().Select(t => t.Tile).ToList(), mapConfiguration.XSize, mapConfiguration.ZSize);
            }

            return _map;
        }
    }
}
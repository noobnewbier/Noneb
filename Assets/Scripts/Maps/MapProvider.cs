using System.Linq;
using Common.Providers;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps
{
    public class MapProvider : MonoObjectProvider<Map>
    {
        private Map _map;
        [SerializeField] private MapConfiguration mapConfiguration;
        [FormerlySerializedAs("tileRepresentationRepositoryProvider")] [SerializeField] private TileHolderRepositoryProvider tileHolderRepositoryProvider;

        public override Map Provide()
        {
            if (_map == null)
            {
                var tileHoldersRepository = tileHolderRepositoryProvider.Provide();
                _map = new Map(tileHoldersRepository.GetAllFlatten().Select(t => t.Value).ToList(), mapConfiguration.XSize, mapConfiguration.ZSize);
            }

            return _map;
        }
    }
}
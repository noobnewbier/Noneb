using System.Linq;
using Common.Providers;
using Maps.Repositories;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps
{
    public class MapProvider : MonoObjectProvider<Map>
    {
        private Map _map;

        [FormerlySerializedAs("tileRepresentationRepositoryProvider")] [SerializeField]
        private TileHolderRepositoryProvider tileHolderRepositoryProvider;

        [SerializeField] private MapCharacteristicRepositoryProvider mapCharacteristicRepositoryProvider;


        public override Map Provide()
        {
            if (_map == null)
            {
                var tileHoldersRepository = tileHolderRepositoryProvider.Provide();
                _map = new Map(tileHoldersRepository.GetAllFlatten().Select(t => t.Value).ToList(),
                    mapCharacteristicRepositoryProvider.Provide());
            }

            return _map;
        }
    }
}
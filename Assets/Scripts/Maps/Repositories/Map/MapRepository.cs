using System;
using System.Linq;
using Common;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using Maps.Repositories.CurrentMapConfig;
using Tiles.Holders;
using UniRx;

namespace Maps.Repositories.Map
{
    public interface IMapRepository : IDataGetRepository<Maps.Map>
    {
    }

    public class MapRepository : IMapRepository
    {
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;
        private readonly IBoardItemsHolderGetRepository<TileHolder> _tilesHolderGetRepository;

        public MapRepository(ICurrentMapConfigRepository currentMapConfigRepository,
                             IBoardItemsHolderGetRepository<TileHolder> tilesHolderGetRepository)
        {
            _currentMapConfigRepository = currentMapConfigRepository;
            _tilesHolderGetRepository = tilesHolderGetRepository;
        }

        public IObservable<Maps.Map> GetObservableStream()
        {
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tilesHolderGetRepository.GetObservableStream(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }

        public IObservable<Maps.Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tilesHolderGetRepository.GetMostRecent(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }
    }
}
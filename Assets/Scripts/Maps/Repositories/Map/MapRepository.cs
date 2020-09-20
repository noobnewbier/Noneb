using System;
using System.Linq;
using Common;
using GameEnvironments.Common.Repositories.BoardItemsHolder;
using Maps.Repositories.CurrentMapConfig;
using Tiles.Holders;
using Tiles.Holders.Repository;
using UniRx;

namespace Maps.Repositories.Map
{
    public interface IMapRepository : IDataGetRepository<Maps.Map>
    {
    }

    public class MapRepository : IMapRepository
    {
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;
        private readonly IBoardItemsHolderRepository<TileHolder> _tilesHolderRepository;

        public MapRepository(ICurrentMapConfigRepository currentMapConfigRepository, IBoardItemsHolderRepository<TileHolder> tilesHolderRepository)
        {
            _currentMapConfigRepository = currentMapConfigRepository;
            _tilesHolderRepository = tilesHolderRepository;
        }

        public IObservable<Maps.Map> GetObservableStream()
        {
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tilesHolderRepository.GetObservableStream(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }

        public IObservable<Maps.Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tilesHolderRepository.GetMostRecent(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }
    }
}
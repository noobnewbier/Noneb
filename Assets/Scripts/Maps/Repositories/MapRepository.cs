using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;
using Tiles.Holders.Repository;
using UniRx;

namespace Maps.Repositories
{
    public interface IMapRepository
    {
        IObservable<Map> GetObservableStream();
        IObservable<Map> GetMostRecent();
    }

    public class MapRepository : IMapRepository
    {
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;
        private readonly ITileHoldersRepository _tileHoldersRepository;

        public MapRepository(ICurrentMapConfigRepository currentMapConfigRepository, ITileHoldersRepository tileHoldersRepository)
        {
            _currentMapConfigRepository = currentMapConfigRepository;
            _tileHoldersRepository = tileHoldersRepository;
        }

        public IObservable<Map> GetObservableStream()
        {
            // _tileHoldersRepository.GetAllFlattenSingle().Select(t => t.Value).ToList()
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tileHoldersRepository.GetAllFlattenSingle(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => CreateMap(tuple.config, tuple.tileHolders.Select(t => t.Value).ToList()));
        }

        public IObservable<Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tileHoldersRepository.GetAllFlattenSingle(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => CreateMap(tuple.config, tuple.tileHolders.Select(t => t.Value).ToList()));
        }

        private Map CreateMap(MapConfig config, IReadOnlyList<Tile> tiles)
        {
            return new Map(tiles, config);
        }
    }
}
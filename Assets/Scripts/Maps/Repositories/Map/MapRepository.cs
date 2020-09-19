using System;
using System.Linq;
using Maps.Repositories.CurrentMapConfig;
using Tiles.Holders.Repository;
using UniRx;

namespace Maps.Repositories.Map
{
    public interface IMapRepository
    {
        IObservable<Maps.Map> GetObservableStream();
        IObservable<Maps.Map> GetMostRecent();
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

        public IObservable<Maps.Map> GetObservableStream()
        {
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tileHoldersRepository.GetAllFlattenSingle(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }

        public IObservable<Maps.Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tileHoldersRepository.GetAllFlattenSingle(), (config, tileHolders) => (config, tileHolders))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tileHolders.Select(t => t.Value).ToList(), tuple.config));
        }
    }
}
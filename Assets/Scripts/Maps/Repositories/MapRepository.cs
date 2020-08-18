using System;
using System.Linq;
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
            return _currentMapConfigRepository.GetObservableStream().Where(m => m != null).Select(CreateMap);
        }

        public IObservable<Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent().Where(m => m != null).Select(CreateMap);
        }

        private Map CreateMap(MapConfig config)
        {
            return new Map(_tileHoldersRepository.GetAllFlatten().Select(t => t.Value).ToList(), config);
        }
    }
}

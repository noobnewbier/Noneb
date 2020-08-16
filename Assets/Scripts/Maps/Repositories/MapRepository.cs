using System;
using System.Linq;
using Tiles.Holders.Repository;
using UniRx;

namespace Maps.Repositories
{
    public interface IMapRepository
    {
        IObservable<Map> Get();
    }

    public class MapRepository : IMapRepository
    {
        private readonly IMapConfigurationRepository _mapConfigurationRepository;
        private readonly ITileHoldersRepository _tileHoldersRepository;

        public MapRepository(IMapConfigurationRepository mapConfigurationRepository, ITileHoldersRepository tileHoldersRepository)
        {
            _mapConfigurationRepository = mapConfigurationRepository;
            _tileHoldersRepository = tileHoldersRepository;
        }

        public IObservable<Map> Get()
        {
            return _mapConfigurationRepository.Get().Where(m => m != null).Select(CreateMap);
        }

        private Map CreateMap(MapConfiguration config)
        {
            return new Map(_tileHoldersRepository.GetAllFlatten().Select(t => t.Value).ToList(), config);
        }
    }
}
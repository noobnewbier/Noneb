using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;

namespace Maps.Repositories
{
    public interface IMapConfigurationRepository
    {
        IObservable<MapConfiguration> Get();
    }

    public class MapConfigurationRepository : IMapConfigurationRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _gameEnvironmentGetRepository;

        public MapConfigurationRepository(ICurrentGameEnvironmentGetRepository gameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = gameEnvironmentGetRepository;
        }

        public IObservable<MapConfiguration> Get()
        {
            return _gameEnvironmentGetRepository.Get().Select(d => d.MapConfiguration);
        }
    }
}
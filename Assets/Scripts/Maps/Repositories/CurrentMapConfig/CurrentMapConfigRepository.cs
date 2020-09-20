using System;
using Common;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using UniRx;

namespace Maps.Repositories.CurrentMapConfig
{
    public interface ICurrentMapConfigRepository : IDataGetRepository<MapConfig>
    {
    }

    public class CurrentMapConfigRepository : ICurrentMapConfigRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _gameEnvironmentGetRepository;

        public CurrentMapConfigRepository(ICurrentGameEnvironmentGetRepository gameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = gameEnvironmentGetRepository;
        }

        public IObservable<MapConfig> GetObservableStream()
        {
            return _gameEnvironmentGetRepository.GetObservableStream().Select(d => d.MapConfiguration);
        }

        public IObservable<MapConfig> GetMostRecent()
        {
            return _gameEnvironmentGetRepository.GetMostRecent().Select(d => d.MapConfiguration);
        }
    }
}
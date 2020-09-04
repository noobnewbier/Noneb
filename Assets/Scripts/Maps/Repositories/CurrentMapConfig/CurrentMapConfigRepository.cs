using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;

namespace Maps.Repositories.CurrentMapConfig
{
    public interface ICurrentMapConfigRepository
    {
        IObservable<MapConfig> GetObservableStream();
        IObservable<MapConfig> GetMostRecent();
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
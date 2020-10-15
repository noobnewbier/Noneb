using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using Noneb.Core.Game.Maps;
using UniRx;

namespace Noneb.Core.Game.GameState.CurrentMapConfig
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
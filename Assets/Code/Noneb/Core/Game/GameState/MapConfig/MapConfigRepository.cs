using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameState.GameEnvironments;
using UniRx;

namespace Noneb.Core.Game.GameState.MapConfig
{
    public interface IMapConfigRepository : IDataGetRepository<Game.Maps.MapConfig>
    {
    }

    public class MapConfigRepository : IMapConfigRepository
    {
        private readonly IGameEnvironmentGetRepository _gameEnvironmentGetRepository;

        public MapConfigRepository(IGameEnvironmentGetRepository gameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = gameEnvironmentGetRepository;
        }

        public IObservable<Game.Maps.MapConfig> GetObservableStream()
        {
            return _gameEnvironmentGetRepository.GetObservableStream().Select(d => d.MapConfiguration);
        }

        public IObservable<Game.Maps.MapConfig> GetMostRecent()
        {
            return _gameEnvironmentGetRepository.GetMostRecent().Select(d => d.MapConfiguration);
        }
    }
}
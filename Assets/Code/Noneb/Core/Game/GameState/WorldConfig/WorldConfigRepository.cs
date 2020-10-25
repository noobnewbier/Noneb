using System;
using Noneb.Core.Game.GameState.GameEnvironments;
using UniRx;

namespace Noneb.Core.Game.GameState.WorldConfig
{
    public interface IWorldConfigRepository
    {
        IObservable<WorldConfigurations.WorldConfig> GetObservableStream();
        IObservable<WorldConfigurations.WorldConfig> GetMostRecent();
    }

    public class WorldConfigRepository : IWorldConfigRepository
    {
        private readonly IGameEnvironmentGetRepository _getRepository;

        public WorldConfigRepository(IGameEnvironmentGetRepository getRepository)
        {
            _getRepository = getRepository;
        }

        public IObservable<WorldConfigurations.WorldConfig> GetObservableStream()
        {
            return _getRepository.GetObservableStream().Select(d => d.WorldConfiguration);
        }

        public IObservable<WorldConfigurations.WorldConfig> GetMostRecent()
        {
            return _getRepository.GetMostRecent().Select(d => d.WorldConfiguration);
        }
    }
}
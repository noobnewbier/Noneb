using System;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using Noneb.Core.Game.WorldConfigurations;
using UniRx;

namespace Noneb.Core.Game.GameState.CurrentWorldConfig
{
    public interface IWorldConfigRepository
    {
        IObservable<WorldConfig> GetObservableStream();
        IObservable<WorldConfig> GetMostRecent();
    }

    public class WorldConfigRepository : IWorldConfigRepository
    {
        private readonly IGameEnvironmentGetRepository _getRepository;

        public WorldConfigRepository(IGameEnvironmentGetRepository getRepository)
        {
            _getRepository = getRepository;
        }

        public IObservable<WorldConfig> GetObservableStream()
        {
            return _getRepository.GetObservableStream().Select(d => d.WorldConfiguration);
        }

        public IObservable<WorldConfig> GetMostRecent()
        {
            return _getRepository.GetMostRecent().Select(d => d.WorldConfiguration);
        }
    }
}
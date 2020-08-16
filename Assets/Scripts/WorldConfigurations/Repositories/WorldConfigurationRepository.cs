using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;

namespace WorldConfigurations.Repositories
{
    public interface IWorldConfigurationRepository
    {
        IObservable<WorldConfiguration> Get();
    }

    public class WorldConfigurationRepository : IWorldConfigurationRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _getRepository;

        public WorldConfigurationRepository(ICurrentGameEnvironmentGetRepository getRepository)
        {
            _getRepository = getRepository;
        }

        public IObservable<WorldConfiguration> Get()
        {
            return _getRepository.Get().Select(d => d.WorldConfiguration);
        }
    }
}
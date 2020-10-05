using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using UniRx;

namespace WorldConfigurations.Repositories
{
    public interface ICurrentWorldConfigRepository
    {
        IObservable<WorldConfig> GetObservableStream();
        IObservable<WorldConfig> GetMostRecent();
    }

    public class CurrentWorldConfigRepository : ICurrentWorldConfigRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _getRepository;

        public CurrentWorldConfigRepository(ICurrentGameEnvironmentGetRepository getRepository)
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
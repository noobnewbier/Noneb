using GameEnvironments.Common.Repositories.CurrentGameEnvironment;

namespace WorldConfigurations.Repositories
{
    public interface IWorldConfigurationRepository
    {
        WorldConfiguration Get();
    }

    public class WorldConfigurationRepository : IWorldConfigurationRepository
    {
        private readonly ICurrentGameEnvironmentRepository _repository;

        public WorldConfigurationRepository(ICurrentGameEnvironmentRepository repository)
        {
            _repository = repository;
        }

        public WorldConfiguration Get() => _repository.Get().WorldConfiguration;
    }
}
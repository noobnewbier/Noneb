using GameEnvironments.Common.Repositories.CurrentGameEnvironment;

namespace Maps.Repositories
{
    public interface IMapConfigurationRepository
    {
        MapConfiguration Get();
    }

    public class MapConfigurationRepository : IMapConfigurationRepository
    {
        private readonly ICurrentGameEnvironmentRepository _gameEnvironmentRepository;

        public MapConfigurationRepository(ICurrentGameEnvironmentRepository gameEnvironmentRepository)
        {
            _gameEnvironmentRepository = gameEnvironmentRepository;
        }

        public MapConfiguration Get() => _gameEnvironmentRepository.Get().MapConfiguration;
    }
}
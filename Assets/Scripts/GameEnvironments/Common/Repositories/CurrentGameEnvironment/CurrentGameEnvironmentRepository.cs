using GameEnvironments.Common.Data;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    public interface ICurrentGameEnvironmentRepository
    {
        GameEnvironment Get();
    }

    public class CurrentGameEnvironmentRepository : ICurrentGameEnvironmentRepository
    {
        private readonly GameEnvironment _environment;

        public CurrentGameEnvironmentRepository(GameEnvironment environment)
        {
            _environment = environment;
        }

        public GameEnvironment Get() => _environment;
    }
}
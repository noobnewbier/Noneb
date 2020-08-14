using GameEnvironments.Common.Data;
using UnityUtils.ScriptableReference;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    public interface ICurrentGameEnvironmentRepository
    {
        GameEnvironment Get();
    }

    public class CurrentGameEnvironmentRepository : ICurrentGameEnvironmentRepository
    {
        private readonly RuntimeReference<GameEnvironment> _environment;

        public CurrentGameEnvironmentRepository(RuntimeReference<GameEnvironment> environment)
        {
            _environment = environment;
        }


        public GameEnvironment Get() => _environment.CurrentReference;
    }
}
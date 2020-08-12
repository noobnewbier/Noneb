using System.Collections.Generic;
using System.Linq;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Common.Repositories.AvailableGameEnvironment
{
    public interface IAvailableGameEnvironmentRepository
    {
        IEnumerable<GameEnvironment> GameEnvironments { get; }
    }

    public class AvailableGameEnvironmentRepository : IAvailableGameEnvironmentRepository
    {
        public AvailableGameEnvironmentRepository(IEnumerable<GameEnvironmentScriptable> gameEnvironments)
        {
            GameEnvironments = gameEnvironments.Select(e => e.ToGameEnvironment());
        }

        public IEnumerable<GameEnvironment> GameEnvironments { get; }
    }
}
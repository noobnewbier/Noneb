using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameEnvironments.Data;

namespace Noneb.Core.Game.GameState.CurrentGameEnvironments
{
    public interface ICurrentGameEnvironmentRepository : ICurrentGameEnvironmentGetRepository, ICurrentGameEnvironmentSetRepository
    {
    }

    public interface ICurrentGameEnvironmentGetRepository : IDataGetRepository<GameEnvironment>
    {
    }

    public interface ICurrentGameEnvironmentSetRepository : IDataSetRepository<GameEnvironment>
    {
    }

    public class CurrentGameEnvironmentRepository : DataRepository<GameEnvironment>,
                                                    ICurrentGameEnvironmentRepository
    {
    }
}
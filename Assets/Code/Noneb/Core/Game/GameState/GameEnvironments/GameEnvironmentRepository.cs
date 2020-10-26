using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameEnvironments.Data;

namespace Noneb.Core.Game.GameState.GameEnvironments
{
    public interface IGameEnvironmentRepository : IGameEnvironmentGetRepository, IGameEnvironmentSetRepository
    {
    }

    public interface IGameEnvironmentGetRepository : IDataGetRepository<GameEnvironment>
    {
    }

    public interface IGameEnvironmentSetRepository : IDataSetRepository<GameEnvironment>
    {
    }

    public class GameEnvironmentRepository : DataRepository<GameEnvironment>,
                                             IGameEnvironmentRepository
    {
    }
}
using Main.Core.Game.Common;
using Main.Core.Game.GameEnvironments.Data;

namespace Main.Core.Game.GameState.CurrentGameEnvironments
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
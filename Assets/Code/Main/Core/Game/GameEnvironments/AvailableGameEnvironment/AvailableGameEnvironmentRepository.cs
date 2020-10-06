using System.Collections.Generic;
using Main.Core.Game.Common;
using Main.Core.Game.GameEnvironments.Data;

namespace Main.Core.Game.GameEnvironments.AvailableGameEnvironment
{
    public interface IAvailableGameEnvironmentGetRepository : IDataGetRepository<IEnumerable<GameEnvironment>>
    {
    }

    public interface IAvailableGameEnvironmentSetRepository : IDataSetRepository<IEnumerable<GameEnvironment>>
    {
    }

    public interface IAvailableGameEnvironmentRepository : IAvailableGameEnvironmentSetRepository, IAvailableGameEnvironmentGetRepository
    {
    }

    public class AvailableGameEnvironmentRepository : DataRepository<IEnumerable<GameEnvironment>>, IAvailableGameEnvironmentRepository
    {
    }
}
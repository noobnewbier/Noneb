using System.Collections.Generic;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameEnvironments.Data;

namespace Noneb.Core.Game.GameEnvironments.AvailableGameEnvironment
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
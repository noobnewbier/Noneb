using System.Collections.Generic;
using System.Linq;
using Common;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Common.Repositories.AvailableGameEnvironment
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
﻿using Common;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
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
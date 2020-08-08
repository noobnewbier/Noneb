﻿using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.LevelDatas;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class ConstructGameObjectLoader : GameObjectLoader
    {
        protected override ImmutableArray<GameObjectProvider> GetGameObjectProvidersFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.ConstructGameObjectProviders;
        }
    }
}
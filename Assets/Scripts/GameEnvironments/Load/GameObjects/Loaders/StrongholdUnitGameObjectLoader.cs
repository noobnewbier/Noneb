﻿using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        protected override ImmutableArray<GameObjectProvider> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.StrongholdUnitGameObjectProviders;
        }
    }
}
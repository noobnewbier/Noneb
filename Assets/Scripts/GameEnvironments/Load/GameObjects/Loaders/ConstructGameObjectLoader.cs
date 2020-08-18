using System;
using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class ConstructGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructGameObjectProviders.ToImmutableArray());
        }
    }
}
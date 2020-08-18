using System;
using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class TileGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.TileGameObjectProviders.ToImmutableArray());
        }
    }
}
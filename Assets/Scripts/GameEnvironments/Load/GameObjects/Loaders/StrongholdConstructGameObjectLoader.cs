using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdConstructGameObjectLoader),
        menuName = ProjectMenuName.Loader + nameof(StrongholdConstructGameObjectLoader)
    )]
    public class StrongholdConstructGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdConstructGameObjectProviders.ToImmutableArray());
        }
    }
}
using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(ConstructGameObjectLoader))]
    public class ConstructGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructGameObjectProviders.ToImmutableArray());
        }
    }
}
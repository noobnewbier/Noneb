using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(UnitGameObjectLoader))]
    public class UnitGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitGameObjectProviders.ToImmutableArray());
        }
    }
}
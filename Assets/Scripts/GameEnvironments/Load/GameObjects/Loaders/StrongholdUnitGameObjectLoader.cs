using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdUnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdUnitGameObjectLoader))]
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdUnitGameObjectProviders.ToImmutableArray());
        }
    }
}
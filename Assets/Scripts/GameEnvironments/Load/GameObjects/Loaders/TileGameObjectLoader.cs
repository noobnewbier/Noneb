using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(TileGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(TileGameObjectLoader))]
    public class TileGameObjectLoader : GameObjectLoader
    {
        protected override IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.TileGameObjectProviders.ToImmutableArray());
        }
    }
}
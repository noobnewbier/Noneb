using System;
using System.Collections.Generic;
using Common;
using Common.Holders;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolder.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdUnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdUnitGameObjectLoader))]
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        [SerializeField] private StrongholdsHolderRepositoryProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdUnitGameObjectProviders);
        }

        protected override IDataGetRepository<IReadOnlyList<IBoardItemHolder>> GetBoardItemsHolderRepository()
        {
            return repositoryProvider.Provide();
        }
    }
}
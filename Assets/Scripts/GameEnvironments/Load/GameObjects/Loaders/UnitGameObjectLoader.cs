using System;
using System.Collections.Generic;
using Common;
using Common.Holders;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(UnitGameObjectLoader))]
    public class UnitGameObjectLoader : GameObjectLoader
    {
        [SerializeField] private UnitsHolderRepositoryProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitGameObjectProviders);
        }

        protected override IDataGetRepository<IReadOnlyList<IBoardItemHolder>> GetBoardItemsHolderRepository()
        {
            return repositoryProvider.Provide();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using Common;
using Common.Factories;
using Common.Holders;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
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
        [SerializeField] private StrongholdHoldersFetchingServiceRepositoryProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdConstructGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => repositoryProvider.Provide();
    }
}
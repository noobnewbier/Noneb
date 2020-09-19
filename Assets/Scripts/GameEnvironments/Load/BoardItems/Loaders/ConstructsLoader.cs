﻿using System;
using System.Collections.Immutable;
using Constructs;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    public class ConstructsLoader : BoardItemsLoader<ConstructData>
    {
        [SerializeField] private LoadConstructsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<ConstructData> GetService()
        {
            return serviceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<ConstructData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructDatas.ToImmutableArray());
        }
    }
}
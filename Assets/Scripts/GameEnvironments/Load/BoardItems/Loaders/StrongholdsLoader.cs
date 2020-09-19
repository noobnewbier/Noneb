﻿using System;
using System.Collections.Immutable;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItems.Providers;
using Strongholds;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    public class StrongholdsLoader : BoardItemsLoader<StrongholdData>
    {
        [SerializeField] private LoadStrongholdsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<StrongholdData> GetService()
        {
            return serviceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<StrongholdData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdDatas.ToImmutableArray());
        }
    }
}
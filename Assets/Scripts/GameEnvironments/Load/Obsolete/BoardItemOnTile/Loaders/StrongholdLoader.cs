﻿using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.Obsolete.BoardItemOnTile.ServiceProviders;
using Strongholds;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Obsolete.BoardItemOnTile.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdLoader))]
    public class StrongholdLoader : BoardItemOnTileLoader<StrongholdHolder, Stronghold, StrongholdData>
    {
        [SerializeField] private LoadStrongholdServiceProvider loadStrongholdServiceProvider;
        [SerializeField] private StrongholdHolderProvider strongholdHolderProvider;

        protected override ILoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData> GetService()
        {
            return loadStrongholdServiceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<StrongholdData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdDatas.ToImmutableArray());
        }

        protected override IGameObjectAndComponentProvider<StrongholdHolder> GetHolderProvider()
        {
            return strongholdHolderProvider;
        }
    }
}
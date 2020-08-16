using System;
using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using Strongholds;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
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
            return currentLevelDataRepository.Get().Select(d => d.StrongholdDatas.ToImmutableArray());
        }

        protected override IGameObjectAndComponentProvider<StrongholdHolder> GetHolderProvider()
        {
            return strongholdHolderProvider;
        }
    }
}
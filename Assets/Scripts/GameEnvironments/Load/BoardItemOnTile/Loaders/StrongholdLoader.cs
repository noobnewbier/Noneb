using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using Strongholds;
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

        protected override ImmutableArray<StrongholdData> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.StrongholdDatas;
        }

        protected override IGameObjectAndComponentProvider<StrongholdHolder> GetHolderProvider()
        {
            return strongholdHolderProvider;
        }
    }
}
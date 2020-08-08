using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.GameEnvironments;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Repositories.LevelDatas;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using Strongholds;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public class StrongholdLoader: BoardItemOnTileLoader<StrongholdHolder, Stronghold, StrongholdData>
    {
        [SerializeField] private LoadStrongholdServiceProvider loadStrongholdServiceProvider;
        [SerializeField] private StrongholdHolderProvider strongholdHolderProvider;
        
        protected override ILoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData> GetService()
        {
            return loadStrongholdServiceProvider.Provide();
        }

        protected override ImmutableArray<StrongholdData> GetDatasFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.StrongholdDatas;
        }

        protected override IGameObjectAndComponentProvider<StrongholdHolder> GetHolderProvider()
        {
            return strongholdHolderProvider;
        }
    }
}
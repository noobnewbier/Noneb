using Common.Providers;
using GameEnvironments.Common.Data;
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

        protected override StrongholdData[] GetDatasFromEnvironment(GameEnvironment gameEnvironment)
        {
            return gameEnvironment.StrongholdDatas;
        }

        protected override IGameObjectAndComponentProvider<StrongholdHolder> GetHolderProvider()
        {
            return strongholdHolderProvider;
        }
    }
}
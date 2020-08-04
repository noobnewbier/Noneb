using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public class ConstructLoader : BoardItemOnTileLoader<ConstructHolder, Construct, ConstructData>
    {
        [SerializeField] private LoadConstructServiceProvider loadConstructServiceProvider;
        [SerializeField] private ConstructHolderProvider constructHolderProvider;
        
        protected override ILoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData> GetService()
        {
            return loadConstructServiceProvider.Provide();
        }

        protected override ConstructData[] GetDatasFromEnvironment(GameEnvironment gameEnvironment)
        {
            return gameEnvironment.ConstructDatas;
        }

        protected override IGameObjectAndComponentProvider<ConstructHolder> GetHolderProvider()
        {
            return constructHolderProvider;
        }
    }
}
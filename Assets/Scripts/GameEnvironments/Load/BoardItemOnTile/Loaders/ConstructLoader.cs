using System.Collections.Immutable;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.CurrentLevelData;
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

        protected override ImmutableArray<ConstructData> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.ConstructDatas;
        }

        protected override IGameObjectAndComponentProvider<ConstructHolder> GetHolderProvider()
        {
            return constructHolderProvider;
        }
    }
}
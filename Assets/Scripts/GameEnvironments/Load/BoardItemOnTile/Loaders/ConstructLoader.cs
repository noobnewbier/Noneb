using System;
using System.Collections.Immutable;
using Common;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructLoader), menuName = ProjectMenuName.Loader + nameof(ConstructLoader))]
    public class ConstructLoader : BoardItemOnTileLoader<ConstructHolder, Construct, ConstructData>
    {
        [SerializeField] private LoadConstructServiceProvider loadConstructServiceProvider;
        [SerializeField] private ConstructHolderProvider constructHolderProvider;

        protected override ILoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData> GetService()
        {
            return loadConstructServiceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<ConstructData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructDatas.ToImmutableArray());
        }

        protected override IGameObjectAndComponentProvider<ConstructHolder> GetHolderProvider()
        {
            return constructHolderProvider;
        }
    }
}
using System;
using System.Collections.Immutable;
using Common;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using Strongholds;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdsLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdsLoader))]
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
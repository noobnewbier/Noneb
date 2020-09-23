using System;
using System.Collections.Immutable;
using Common;
using Constructs.Data;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructsLoader), menuName = ProjectMenuName.Loader + nameof(ConstructsLoader))]
    public class ConstructsLoader : BoardItemsLoader<ConstructData>
    {
        [SerializeField] private LoadConstructsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<ConstructData> GetService()
        {
            return serviceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<ConstructData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructDatas.ToImmutableArray());
        }
    }
}
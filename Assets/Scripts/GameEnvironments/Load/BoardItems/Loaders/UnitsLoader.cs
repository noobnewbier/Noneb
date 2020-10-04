using System;
using System.Collections.Immutable;
using Common;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using Units.Data;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitsLoader), menuName = ProjectMenuName.Loader + nameof(UnitsLoader))]
    public class UnitsLoader : BoardItemsLoader<UnitData>
    {
        [SerializeField] private LoadUnitsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<UnitData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<UnitData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitDatas.ToImmutableArray());
        }
    }
}
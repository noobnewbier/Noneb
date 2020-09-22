using System;
using System.Collections.Immutable;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using Units;
using Units.Data;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    public class UnitsLoader : BoardItemsLoader<UnitData>
    {
        [SerializeField] private LoadUnitsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<UnitData> GetService()
        {
            return serviceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<UnitData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitDatas.ToImmutableArray());
        }
    }
}
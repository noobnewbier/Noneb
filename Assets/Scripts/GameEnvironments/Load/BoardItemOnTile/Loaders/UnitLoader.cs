using System;
using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using UniRx;
using Units;
using Units.Holders;
using UnityEngine;
using Unit = Units.Unit;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public class UnitLoader : BoardItemOnTileLoader<UnitHolder, Unit, UnitData>
    {
        [SerializeField] private LoadUnitServiceProvider loadUnitServiceProvider;
        [SerializeField] private UnitHolderProvider unitHolderProvider;

        protected override ILoadBoardItemOnTileService<UnitHolder, Unit, UnitData> GetService()
        {
            return loadUnitServiceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<UnitData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitDatas.ToImmutableArray());
        }

        protected override IGameObjectAndComponentProvider<UnitHolder> GetHolderProvider()
        {
            return unitHolderProvider;
        }
    }
}
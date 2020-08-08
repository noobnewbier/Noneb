using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.LevelDatas;
using GameEnvironments.Load.BoardItemOnTile.ServiceProviders;
using Units;
using Units.Holders;
using UnityEngine;

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

        protected override ImmutableArray<UnitData> GetDatasFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.UnitDatas;
        }

        protected override IGameObjectAndComponentProvider<UnitHolder> GetHolderProvider()
        {
            return unitHolderProvider;
        }
    }
}
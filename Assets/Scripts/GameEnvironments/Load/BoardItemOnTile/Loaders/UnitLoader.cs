using Common.Providers;
using GameEnvironments.Common.Data;
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

        protected override UnitData[] GetDatasFromEnvironment(GameEnvironment gameEnvironment)
        {
            return gameEnvironment.UnitDatas;
        }

        protected override IGameObjectAndComponentProvider<UnitHolder> GetHolderProvider()
        {
            return unitHolderProvider;
        }
    }
}
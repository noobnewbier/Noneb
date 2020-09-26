using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Services;
using Units;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadUnitsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitsHolderService")]
    public class LoadUnitsHolderServiceProvider : ScriptableObjectProvider<LoadBoardItemsHolderService<UnitHolder, Unit>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;
        [SerializeField] private UnitHolderProvider unitHolderProvider;
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<UnitHolder, Unit> _cache;

        public override LoadBoardItemsHolderService<UnitHolder, Unit> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsHolderService<UnitHolder, Unit>(
                tilesPositionServiceProvider.Provide(),
                unitsRepositoryProvider.Provide(),
                unitHolderProvider,
                coordinateServiceProvider.Provide()
            ));
        }
    }
}
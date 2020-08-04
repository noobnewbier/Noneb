using Common.Factories;
using Common.Providers;
using Maps;
using Maps.Services;
using Units;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItemOnTile.ServiceProviders
{
    [CreateAssetMenu(fileName = nameof(LoadUnitServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitService")]
    public class LoadUnitServiceProvider : ScriptableObjectProvider<ILoadBoardItemOnTileService<UnitHolder, Unit, UnitData>>
    {
        [SerializeField] private GetCoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemOnTileService<UnitHolder, Unit, UnitData> _onTileService;

        private void OnEnable()
        {
            _onTileService = new LoadBoardItemOnTileService<UnitHolder, Unit, UnitData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<UnitData, Coordinate, Unit>((data, coordinate) => new Unit(data, coordinate))
            );
        }

        public override ILoadBoardItemOnTileService<UnitHolder, Unit, UnitData> Provide()
        {
            return _onTileService;
        }
    }
}
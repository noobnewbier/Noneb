using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.BoardItems.Providers;
using Noneb.Core.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadUnitsServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitsService")]
    public class LoadUnitsServiceProvider : ScriptableObject, IObjectProvider<ILoadBoardItemsService<UnitData>>
    {
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemsService<UnitData> _cache;

        public ILoadBoardItemsService<UnitData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Unit, UnitData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<UnitData, Coordinate, Unit>
                    ((data, coordinate) => new Unit(data, coordinate)),
                unitsRepositoryProvider.Provide()
            ));
        }
    }
}
﻿using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadUnitsServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitsService")]
    public class LoadUnitsServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Unit, UnitData>>
    {
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsService<Unit, UnitData> _cache;

        public override LoadBoardItemsService<Unit, UnitData> Provide()
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
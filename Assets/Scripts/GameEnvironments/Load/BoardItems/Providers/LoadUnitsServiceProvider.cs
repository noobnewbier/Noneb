﻿using Common.Factories;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps;
using Maps.Services;
using Units;
using Units.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadUnitsServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitsService")]
    public class LoadUnitsServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Unit, UnitData>>
    {
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;
        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

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
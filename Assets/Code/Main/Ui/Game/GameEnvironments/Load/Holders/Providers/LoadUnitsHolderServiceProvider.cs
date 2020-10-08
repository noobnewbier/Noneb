using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.Units;
using Main.Ui.Game.Maps.TilesPosition;
using Main.Ui.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadUnitsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadUnitsHolderService")]
    public class LoadUnitsHolderServiceProvider : ScriptableObject, IObjectProvider<LoadBoardItemsHolderService<UnitHolder, Unit>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private UnitsRepositoryProvider unitsRepositoryProvider;

        [FormerlySerializedAs("unitHolderProvider")] [SerializeField]
        private UnitHolderFactory unitHolderFactory;

        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<UnitHolder, Unit> _cache;

        public LoadBoardItemsHolderService<UnitHolder, Unit> Provide() =>
            _cache ?? (_cache = new LoadBoardItemsHolderService<UnitHolder, Unit>(
                tilesPositionServiceProvider.Provide(),
                unitsRepositoryProvider.Provide(),
                unitHolderFactory,
                coordinateServiceProvider.Provide()
            ));
    }
}
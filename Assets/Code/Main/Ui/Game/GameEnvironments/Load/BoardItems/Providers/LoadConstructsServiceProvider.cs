﻿using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Constructs;
using Main.Core.Game.Constructs.Data;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Maps.Coordinate.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructsService")]
    public class LoadConstructsServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Construct, ConstructData>>
    {
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsService<Construct, ConstructData> _cache;

        public override LoadBoardItemsService<Construct, ConstructData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Construct, ConstructData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<ConstructData, Coordinate, Construct>
                    ((data, coordinate) => new Construct(data, coordinate)),
                constructsRepositoryProvider.Provide()
            ));
        }
    }
}
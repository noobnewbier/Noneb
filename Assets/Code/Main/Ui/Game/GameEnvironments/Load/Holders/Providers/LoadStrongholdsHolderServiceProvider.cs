using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Strongholds;
using Main.Ui.Game.Maps.TilesPosition;
using Main.Ui.Game.Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadStrongholdsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadStrongholdsHolderService")]
    public class LoadStrongholdsHolderServiceProvider : ScriptableObject, IObjectProvider<LoadBoardItemsHolderService<StrongholdHolder, Stronghold>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        [FormerlySerializedAs("strongholdHolderProvider")] [SerializeField]
        private StrongholdHolderFactory strongholdHolderFactory;

        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<StrongholdHolder, Stronghold> _cache;

        public LoadBoardItemsHolderService<StrongholdHolder, Stronghold> Provide() =>
            _cache ?? (_cache = new LoadBoardItemsHolderService<StrongholdHolder, Stronghold>(
                tilesPositionServiceProvider.Provide(),
                strongholdsRepositoryProvider.Provide(),
                strongholdHolderFactory,
                coordinateServiceProvider.Provide()
            ));
    }
}
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Maps.Coordinate;
using Main.Core.Game.Maps.Coordinate.Services;
using Main.Core.Game.Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadStrongholdsServiceProvider), menuName = MenuName.ScriptableService + "LoadStrongholdsService")]
    public class LoadStrongholdsServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Stronghold, StrongholdData>>
    {
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsService<Stronghold, StrongholdData> _cache;

        public override LoadBoardItemsService<Stronghold, StrongholdData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Stronghold, StrongholdData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<StrongholdData, Coordinate, Stronghold>
                    ((data, coordinate) => new Stronghold(data, coordinate)),
                strongholdsRepositoryProvider.Provide()
            ));
        }
    }
}
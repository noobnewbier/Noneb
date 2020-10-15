using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.BoardItems.Providers;
using Noneb.Core.Game.Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadStrongholdsServiceProvider), menuName = MenuName.ScriptableService + "LoadStrongholdsService")]
    public class LoadStrongholdsServiceProvider : ScriptableObject, IObjectProvider<ILoadBoardItemsService<StrongholdData>>
    {
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemsService<StrongholdData> _cache;

        public ILoadBoardItemsService<StrongholdData> Provide()
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
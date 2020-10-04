using Common.Factories;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps;
using Maps.Services;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItems.Providers
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
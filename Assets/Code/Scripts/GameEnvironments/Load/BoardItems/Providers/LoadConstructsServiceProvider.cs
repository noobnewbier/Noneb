using Common.Factories;
using Common.Providers;
using Constructs;
using Constructs.Data;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps;
using Maps.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItems.Providers
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
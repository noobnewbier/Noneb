using Common.Factories;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructsService")]
    public class LoadConstructsServiceProvider : ScriptableObjectProvider<LoadBoardItemsService<Construct, ConstructData>>
    {
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;
        [SerializeField] private GetCoordinateServiceProvider getCoordinateServiceProvider;

        private LoadBoardItemsService<Construct, ConstructData> _cache;

        public override LoadBoardItemsService<Construct, ConstructData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Construct, ConstructData>(
                getCoordinateServiceProvider.Provide(),
                Factory.Create<ConstructData, Coordinate, Construct>
                    ((data, coordinate) => new Construct(data, coordinate)),
                constructsRepositoryProvider.Provide()
            ));
        }
    }
}
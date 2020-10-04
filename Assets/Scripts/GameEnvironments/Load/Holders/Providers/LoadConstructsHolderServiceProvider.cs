using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructsHolderService")]
    public class LoadConstructsHolderServiceProvider : ScriptableObjectProvider<LoadBoardItemsHolderService<ConstructHolder, Construct>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;
        [FormerlySerializedAs("constructHolderProvider")] [SerializeField] private ConstructHolderFactory constructHolderFactory;
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<ConstructHolder, Construct> _cache;

        public override LoadBoardItemsHolderService<ConstructHolder, Construct> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsHolderService<ConstructHolder, Construct>(
                tilesPositionServiceProvider.Provide(),
                constructsRepositoryProvider.Provide(),
                constructHolderFactory,
                coordinateServiceProvider.Provide()
            ));
        }
    }
}
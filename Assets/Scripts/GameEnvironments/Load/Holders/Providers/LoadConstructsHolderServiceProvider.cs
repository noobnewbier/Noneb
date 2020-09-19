using Common.Providers;
using Constructs;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsHolderServiceProvider), menuName = MenuName.ScriptableService + "ConstructsHolderService")]
    public class LoadConstructsHolderServiceProvider : ScriptableObjectProvider<LoadBoardItemsHolderService<ConstructHolder, Construct>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;
        [SerializeField] private ConstructHolderProvider constructHolderProvider;


        private LoadBoardItemsHolderService<ConstructHolder, Construct> _cache;

        public override LoadBoardItemsHolderService<ConstructHolder, Construct> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsHolderService<ConstructHolder, Construct>(
                tilesPositionServiceProvider.Provide(),
                constructsRepositoryProvider.Provide(),
                constructHolderProvider
            ));
        }
    }
}
using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(TilesHolderLoader), menuName = ProjectMenuName.Loader + nameof(TilesHolderLoader))]
    public class TilesHolderLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadTilesHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
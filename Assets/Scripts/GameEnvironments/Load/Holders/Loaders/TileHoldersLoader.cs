using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(TileHoldersLoader), menuName = ProjectMenuName.Loader + nameof(TileHoldersLoader))]
    public class TileHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadTilesHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
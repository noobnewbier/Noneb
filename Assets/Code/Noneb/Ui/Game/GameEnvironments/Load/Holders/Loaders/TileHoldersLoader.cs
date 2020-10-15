using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(TileHoldersLoader), menuName = ProjectMenuName.Loader + nameof(TileHoldersLoader))]
    public class TileHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadTilesHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService() => serviceProvider.Provide();
    }
}
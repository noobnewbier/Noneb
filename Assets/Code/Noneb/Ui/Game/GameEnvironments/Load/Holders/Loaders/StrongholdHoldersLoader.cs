using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdHoldersLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdHoldersLoader))]
    public class StrongholdHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadStrongholdsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService() => serviceProvider.Provide();
    }
}
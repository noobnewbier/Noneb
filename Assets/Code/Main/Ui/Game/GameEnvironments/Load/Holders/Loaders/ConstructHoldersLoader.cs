using Main.Core.Game.Common.Constants;
using Main.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructHoldersLoader), menuName = ProjectMenuName.Loader + nameof(ConstructHoldersLoader))]
    public class ConstructHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadConstructsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService() => serviceProvider.Provide();
    }
}
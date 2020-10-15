using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitHoldersLoader), menuName = ProjectMenuName.Loader + nameof(UnitHoldersLoader))]
    public class UnitHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadUnitsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService() => serviceProvider.Provide();
    }
}
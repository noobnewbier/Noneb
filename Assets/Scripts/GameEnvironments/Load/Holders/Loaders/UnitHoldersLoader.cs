using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitHoldersLoader), menuName = ProjectMenuName.Loader + nameof(UnitHoldersLoader))]
    public class UnitHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadUnitsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
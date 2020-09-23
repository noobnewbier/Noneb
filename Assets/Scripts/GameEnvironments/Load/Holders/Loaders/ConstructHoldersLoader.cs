using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructHoldersLoader), menuName = ProjectMenuName.Loader + nameof(ConstructHoldersLoader))]
    public class ConstructHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadConstructsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
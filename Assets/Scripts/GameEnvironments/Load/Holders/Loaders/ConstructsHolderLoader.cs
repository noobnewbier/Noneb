using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructsHolderLoader), menuName = ProjectMenuName.Loader + nameof(ConstructsHolderLoader))]
    public class ConstructsHolderLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadConstructsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
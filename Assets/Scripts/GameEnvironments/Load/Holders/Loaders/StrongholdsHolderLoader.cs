using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdsHolderLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdsHolderLoader))]
    public class StrongholdsHolderLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadStrongholdsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
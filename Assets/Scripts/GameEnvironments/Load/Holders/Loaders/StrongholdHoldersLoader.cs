using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdHoldersLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdHoldersLoader))]
    public class StrongholdHoldersLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadStrongholdsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
using Common;
using GameEnvironments.Load.Holders.Providers;
using UnityEngine;

namespace GameEnvironments.Load.Holders.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitsHolderLoader), menuName = ProjectMenuName.Loader + nameof(UnitsHolderLoader))]
    public class UnitsHolderLoader : BoardItemsHolderLoader
    {
        [SerializeField] private LoadUnitsHolderServiceProvider serviceProvider;

        protected override ILoadBoardItemsHolderService GetService()
        {
            return serviceProvider.Provide();
        }
    }
}
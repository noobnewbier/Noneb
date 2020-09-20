using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Tiles;
using UnityEngine;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class TilesHolderProviderSetter : MonoBehaviour
    {
        [SerializeField] private TilesHolderProviderRepositoryProvider repositoryProvider;
        [SerializeField] private TilesHolderProvider holdersProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersProvider);
        }
    }
}
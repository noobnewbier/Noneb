using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Strongholds;
using Tiles;
using Units;
using UnityEngine;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class StrongholdsHolderProviderSetter : MonoBehaviour
    {
        [SerializeField] private StrongholdsHolderProviderRepositoryProvider repositoryProvider;
        [SerializeField] private StrongholdsHolderProvider holdersProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersProvider);
        }
    }
}
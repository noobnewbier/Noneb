using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using UnityEngine;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class ConstructsHolderProviderSetter : MonoBehaviour
    {
        [SerializeField] private ConstructsHolderProviderRepositoryProvider repositoryProvider;
        [SerializeField] private ConstructsHolderProvider holdersProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersProvider);
        }
    }
}
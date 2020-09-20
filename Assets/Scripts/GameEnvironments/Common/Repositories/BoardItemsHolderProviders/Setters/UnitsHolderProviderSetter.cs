using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Units;
using UnityEngine;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class UnitsHolderProviderSetter : MonoBehaviour
    {
        [SerializeField] private UnitsHolderProviderRepositoryProvider repositoryProvider;
        [SerializeField] private UnitsHolderProvider holdersProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersProvider);
        }
    }
}
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class UnitsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private UnitsHolderFetcherRepositoryProvider repositoryProvider;
        [FormerlySerializedAs("holdersProvider")] [SerializeField] private UnitsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}
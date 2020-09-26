using Constructs;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class ConstructsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private ConstructsHolderFetcherRepositoryProvider repositoryProvider;
        [FormerlySerializedAs("holdersProvider")] [SerializeField] private ConstructsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}
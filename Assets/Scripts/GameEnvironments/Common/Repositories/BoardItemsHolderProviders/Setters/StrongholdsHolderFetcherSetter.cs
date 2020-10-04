using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class StrongholdsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private StrongholdsHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("holdersProvider")] [SerializeField]
        private StrongholdsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}
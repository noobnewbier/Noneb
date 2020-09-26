using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters
{
    public class TilesHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private TilesHolderFetcherRepositoryProvider repositoryProvider;
        [FormerlySerializedAs("holdersProvider")] [SerializeField] private TilesHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}
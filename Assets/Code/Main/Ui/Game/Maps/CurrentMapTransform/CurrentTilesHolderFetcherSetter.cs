using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps
{
    public class CurrentTilesHolderFetcherSetter : MonoBehaviour
    {
        [FormerlySerializedAs("currentTilesTransformRepositoryProvider")] [SerializeField]
        private TilesHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("tilesHolderProvider")] [FormerlySerializedAs("tilesTransformProvider")] [SerializeField]
        private TilesHolderFetcher tilesHolderFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(tilesHolderFetcher);
        }
    }
}
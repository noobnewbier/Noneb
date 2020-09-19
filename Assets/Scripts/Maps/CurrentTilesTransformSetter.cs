using GameEnvironments.Common.Repositories.BoardItemsHolderProvider.Providers;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps
{
    public class CurrentTilesTransformSetter : MonoBehaviour
    {
        [FormerlySerializedAs("currentTilesTransformRepositoryProvider")] [SerializeField]
        private TilesHolderProviderRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("tilesTransformProvider")] [SerializeField]
        private TilesHolderProvider tilesHolderProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(tilesHolderProvider);
        }
    }
}
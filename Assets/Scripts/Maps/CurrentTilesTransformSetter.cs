using Maps.Repositories.CurrentTilesTransform;
using Tiles;
using UnityEngine;

namespace Maps
{
    public class CurrentTilesTransformSetter : MonoBehaviour
    {
        [SerializeField] private CurrentTilesTransformRepositoryProvider currentTilesTransformRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            currentTilesTransformRepositoryProvider.Provide().Set(tilesTransformProvider);
        }
    }
}
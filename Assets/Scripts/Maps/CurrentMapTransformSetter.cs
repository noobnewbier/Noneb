using Maps.Repositories.CurrentMapTransform;
using UnityEngine;

namespace Maps
{
    public class CurrentMapTransformSetter : MonoBehaviour
    {
        [SerializeField] private CurrentMapTransformRepositoryProvider repositoryProvider;
        [SerializeField] private Transform mapTransform;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(mapTransform);
        }
    }
}
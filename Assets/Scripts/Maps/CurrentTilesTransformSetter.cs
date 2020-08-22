using Maps.Services;
using Tiles;
using UnityEngine;

namespace Maps
{
    public class CurrentTilesTransformSetter : MonoBehaviour
    {
        [SerializeField] private CurrentTilesTransformRepositoryProvider currentTilesTransformRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        private ICurrentTilesTransformSetRepository _setRepository;

        private void OnEnable()
        {
            _setRepository = currentTilesTransformRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            _setRepository.SetTransformProvider(tilesTransformProvider);
        }
    }
}
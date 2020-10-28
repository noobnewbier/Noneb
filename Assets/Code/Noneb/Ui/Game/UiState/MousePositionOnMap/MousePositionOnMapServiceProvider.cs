using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Cameras;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.MousePositionOnMap
{
    [CreateAssetMenu(fileName = nameof(MousePositionOnMapServiceProvider), menuName = MenuName.ScriptableService + nameof(MousePositionOnMapService))]
    public class MousePositionOnMapServiceProvider : ScriptableObject, IObjectProvider<IMousePositionService>
    {
        [SerializeField] private CameraRepositoryProvider cameraRepositoryProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider currentMapTransformRepositoryProvider;
        private IMousePositionService _cache;

        public IMousePositionService Provide() => _cache ?? (_cache = new MousePositionOnMapService(
            cameraRepositoryProvider.Provide(),
            currentMapTransformRepositoryProvider.Provide()
        ));
    }
}
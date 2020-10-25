using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Cameras;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.MousePosition
{
    [CreateAssetMenu(fileName = nameof(MousePositionServiceProvider), menuName = MenuName.ScriptableService + nameof(MousePositionService))]
    public class MousePositionServiceProvider : ScriptableObject, IObjectProvider<IMousePositionService>
    {
        [SerializeField] private CameraRepositoryProvider cameraRepositoryProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider currentMapTransformRepositoryProvider;
        private IMousePositionService _cache;

        public IMousePositionService Provide() => _cache ?? (_cache = new MousePositionService(
            cameraRepositoryProvider.Provide(),
            currentMapTransformRepositoryProvider.Provide()
        ));
    }
}
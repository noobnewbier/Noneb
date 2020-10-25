using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using Noneb.Ui.InGameEditor.Cameras;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.ClickHandlingService
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
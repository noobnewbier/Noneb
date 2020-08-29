using InGameEditor.Repositories.InGameEditorCamera;
using InGameEditor.Services.InGameEditorCameraSizeInView;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Cameras
{
    [CreateAssetMenu(fileName = nameof(InGameEditorCameraViewModelFactory), menuName = MenuName.Factory + nameof(InGameEditorCameraViewModel))]
    public class InGameEditorCameraViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorCameraSizeInViewServiceProvider cameraSizeInViewServiceProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;

        public InGameEditorCameraViewModel Create(Transform mapTransform, InGameEditorCameraConfig config)
        {
            return new InGameEditorCameraViewModel(
                cameraSizeInViewServiceProvider.Provide(),
                mapTransform,
                config,
                tilesPositionServiceProvider.Provide(),
                cameraRepositoryProvider.Provide()
            );
        }
    }
}
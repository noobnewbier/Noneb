using InGameEditor.Services.InGameEditorCameraSizeInViewServices;
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


        public InGameEditorCameraViewModel Create(Camera editorCamera, Transform mapTransform, InGameEditorCameraConfig config)
        {
            return new InGameEditorCameraViewModel(
                cameraSizeInViewServiceProvider.Provide(),
                editorCamera,
                mapTransform,
                config,
                tilesPositionServiceProvider.Provide()
            );
        }
    }
}
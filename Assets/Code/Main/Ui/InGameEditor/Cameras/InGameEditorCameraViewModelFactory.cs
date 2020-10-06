using Main.Ui.Game.Maps.TilesPosition;
using Main.Ui.InGameEditor.Cameras.SizeInView;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.Cameras
{
    [CreateAssetMenu(fileName = nameof(InGameEditorCameraViewModelFactory), menuName = MenuName.Factory + nameof(InGameEditorCameraViewModel))]
    public class InGameEditorCameraViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorCameraSizeInViewServiceProvider cameraSizeInViewServiceProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;

        public InGameEditorCameraViewModel Create(Transform mapTransform, InGameEditorCameraConfig config) =>
            new InGameEditorCameraViewModel(
                cameraSizeInViewServiceProvider.Provide(),
                mapTransform,
                config,
                tilesPositionServiceProvider.Provide(),
                cameraRepositoryProvider.Provide()
            );
    }
}
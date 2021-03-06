﻿using Noneb.Ui.Game.Cameras;
using Noneb.Ui.Game.Cameras.SizeInView;
using Noneb.Ui.Game.Maps.TilesPosition;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Cameras
{
    [CreateAssetMenu(fileName = nameof(InGameEditorCameraViewModelFactory), menuName = MenuName.Factory + nameof(InGameEditorCameraViewModel))]
    public class InGameEditorCameraViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorCameraSizeInViewServiceProvider cameraSizeInViewServiceProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private CameraRepositoryProvider cameraRepositoryProvider;

        public InGameEditorCameraViewModel Create(Transform mapTransform, CameraConfig config) =>
            new InGameEditorCameraViewModel(
                cameraSizeInViewServiceProvider.Provide(),
                mapTransform,
                config,
                tilesPositionServiceProvider.Provide(),
                cameraRepositoryProvider.Provide()
            );
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Services.InGameEditorCameraSizeInViewServices;
using Maps.Services;
using UniRx;
using UnityEngine;

namespace InGameEditor.Cameras
{
    public class InGameEditorCameraViewModel : IDisposable
    {
        public ILiveData<Vector3> CameraPositionLiveData { get; }
        private readonly IInGameEditorCameraSizeInViewService _cameraSizeInViewService;
        private readonly Camera _editorCamera;
        private readonly Transform _mapTransform;
        private readonly InGameEditorCameraConfig _config;
        private readonly IDisposable _disposable;

        private float _upBound;
        private float _downBound;
        private float _rightBound;
        private float _leftBound;

        public InGameEditorCameraViewModel(IInGameEditorCameraSizeInViewService cameraSizeInViewService,
                                           Camera editorCamera,
                                           Transform mapTransform,
                                           InGameEditorCameraConfig config,
                                           ITilesPositionService tilesPositionService)
        {
            _cameraSizeInViewService = cameraSizeInViewService;
            _editorCamera = editorCamera;
            _mapTransform = mapTransform;
            _config = config;

            CameraPositionLiveData = new LiveData<Vector3>();

            _disposable = tilesPositionService
                .GetObservableStream(mapTransform.position.y)
                .Subscribe(CalculateBound);
        }

        public void OnPanning(Vector3 panningDirection, float panningStrength, float deltaTime)
        {
            var currentCameraPosition = _editorCamera.transform.position;
            var newCameraPosition = currentCameraPosition;

            newCameraPosition += panningDirection * (_config.MaxPanningSpeed * deltaTime * panningStrength);

            //clamping
            newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, _leftBound, _rightBound);
            newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, _downBound, _upBound);

            CameraPositionLiveData.PostValue(newCameraPosition);
        }

        private void CalculateBound(IReadOnlyList<Vector3> tilesPositions)
        {
            var (minWidth, distanceToTop, distanceToBottom) =
                _cameraSizeInViewService.GetViewDistanceToFrustumOnPlaneInWorldSpace(_editorCamera, _mapTransform);

            _upBound = tilesPositions.Max(p => p.z) + _config.BufferToClampingEdge - distanceToTop / 2f;
            _downBound = tilesPositions.Min(p => p.z) - _config.BufferToClampingEdge + distanceToBottom / 2f;
            _rightBound = tilesPositions.Max(p => p.x) + _config.BufferToClampingEdge - minWidth / 2f;
            _leftBound = tilesPositions.Min(p => p.x) - _config.BufferToClampingEdge + minWidth / 2f;

            _upBound = Mathf.Max(_upBound, _downBound);
            _rightBound = Mathf.Max(_rightBound, _leftBound);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
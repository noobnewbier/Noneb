﻿using System;
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
        private readonly IInGameEditorCameraSizeInViewService _cameraSizeInViewService;

        private readonly Camera _editorCamera;
        private readonly Transform _mapTransform;
        private readonly InGameEditorCameraConfig _config;
        private readonly IDisposable _disposable;
        private readonly float _minCameraY;

        private float _upBound;
        private float _downBound;
        private float _rightBound;
        private float _leftBound;
        private Vector2 _cameraViewSize;
        private Vector2 _mapSize;
        private readonly BehaviorSubject<float> _cameraYPositionSubject;

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
            
            var mapPosition = mapTransform.position;
            _minCameraY = _config.MinDistanceToMap + mapPosition.y;

            CameraPositionLiveData = new LiveData<Vector3>();


            /*
             * we are trying to avoid having a whole bunch of setting and getting service/repo for the camera
             * but if more class need this thing we will need to refactor
             */
            _cameraYPositionSubject = new BehaviorSubject<float>(_editorCamera.transform.position.y);
            _disposable = new CompositeDisposable
            {
                _cameraYPositionSubject.Subscribe(OnZoomingLevelUpdated),
                tilesPositionService
                    .GetObservableStream(mapPosition.y)
                    .CombineLatest(_cameraYPositionSubject, (positions, _) => positions) //update bounds whenever the camera's height is updated
                    .Subscribe(CalculateCameraRelatedMetrics)
            };
        }

        public ILiveData<Vector3> CameraPositionLiveData { get; } //handles panning, in theory y is never changing through this livedata

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void OnPanning(Vector3 panningDirection, float panningStrength, float deltaTime)
        {
            var currentCameraPosition = _editorCamera.transform.position;
            var newCameraPosition = currentCameraPosition;

            newCameraPosition += panningDirection * (_config.MaxPanningSpeed * deltaTime * panningStrength);

            newCameraPosition = GetClampedCameraPosition(newCameraPosition);

            CameraPositionLiveData.PostValue(newCameraPosition);
        }

        //negative zooming strength for zooming in, positive for zooming out
        public void OnZooming(float zoomingStrength, float deltaTime)
        {
            if (zoomingStrength > 0f)
            {
                if (_cameraViewSize.x > _mapSize.x && _cameraViewSize.y > _mapSize.y && _cameraViewSize.y > _minCameraY)
                {
                    //don't zoom out too far
                    return;
                }
            }

            var currentYPosition = _editorCamera.transform.position.y;
            var newYPosition = currentYPosition + zoomingStrength * deltaTime * _config.MaxZoomingSpeed;
            newYPosition = Mathf.Max(newYPosition, _minCameraY);
            
            _cameraYPositionSubject.OnNext(newYPosition);
        }

        private void OnZoomingLevelUpdated(float newY)
        {
            var currentPosition = CameraPositionLiveData.Value;

            currentPosition.y = newY;
            currentPosition = GetClampedCameraPosition(currentPosition);
            
            CameraPositionLiveData.PostValue(currentPosition);
        }

        private Vector3 GetClampedCameraPosition(Vector3 unclampedPosition)
        {
            unclampedPosition.x = Mathf.Clamp(unclampedPosition.x, _leftBound, _rightBound);
            unclampedPosition.z = Mathf.Clamp(unclampedPosition.z, _downBound, _upBound);
            unclampedPosition.y = Mathf.Max(unclampedPosition.y, _minCameraY);

            return unclampedPosition;
        }

        private void CalculateCameraRelatedMetrics(IReadOnlyList<Vector3> tilesPositions)
        {
            var (minWidth, distanceToTop, distanceToBottom) =
                _cameraSizeInViewService.GetViewDistanceToFrustumOnPlaneInWorldSpace(_editorCamera, _mapTransform);
            var mapMaxHeight = tilesPositions.Max(p => p.z);
            var mapMinHeight = tilesPositions.Min(p => p.z);
            var mapMaxWidth = tilesPositions.Max(p => p.x);
            var mapMinWidth = tilesPositions.Min(p => p.x);

            #region bounds

            _upBound = mapMaxHeight + _config.BufferToClampingEdge - distanceToTop / 2f;
            _downBound = mapMinHeight - _config.BufferToClampingEdge + distanceToBottom / 2f;
            _rightBound = mapMaxWidth + _config.BufferToClampingEdge - minWidth / 2f;
            _leftBound = mapMinWidth - _config.BufferToClampingEdge + minWidth / 2f;

            _upBound = Mathf.Max(_upBound, _downBound);
            _rightBound = Mathf.Max(_rightBound, _leftBound);

            #endregion

            _cameraViewSize = new Vector2(
                minWidth,
                distanceToTop + distanceToBottom
            );
            _mapSize = new Vector2(
                mapMaxWidth - mapMinWidth,
                mapMaxHeight - mapMinHeight
            );
        }
    }
}
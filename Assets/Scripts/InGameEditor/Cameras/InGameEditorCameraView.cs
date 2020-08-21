using System;
using System.Linq;
using InGameEditor.Services.InGameEditorCameraSizeInViewServices;
using Maps.Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace InGameEditor.Cameras
{
    //todo: add zooming
    public class InGameEditorCameraView : MonoBehaviour
    {
        [SerializeField] private Camera editorCamera;
        [SerializeField] private Transform mapTransform;
        [SerializeField] private InGameEditorCameraConfig config;
        [SerializeField] private InGameEditorCameraViewModelFactory viewModelFactory;
                

        private InGameEditorCameraViewModel _viewModel;
        private IDisposable _disposable;
        private float _upBound;
        private float _downBound;
        private float _leftBound;
        private float _rightBound;


        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create(editorCamera, mapTransform, config);
            
            _disposable = new CompositeDisposable(
                _viewModel.CameraPositionLiveData.Subscribe(OnUpdateCameraPosition)
            );
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
            _viewModel?.Dispose();
        }

        private void Update()
        {
            Panning();
        }

        #region Panning

        private void OnUpdateCameraPosition(Vector3 newPosition)
        {
            editorCamera.transform.position = newPosition;
        }

        private void Panning() //consider adding panning with middle mouse button
        {
            var mousePosition = Input.mousePosition;

            var panningDirection = (mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0)).normalized;
            panningDirection.z = panningDirection.y;
            panningDirection.y = 0;

            _viewModel.OnPanning(panningDirection, GetPanningStrength(), Time.deltaTime);
        }

        private float GetPanningStrength()
        {
            var mousePosition = Input.mousePosition;
            var yDistancePercentage = 1f - Mathf.Min(
                mousePosition.y, //to bottom
                Screen.height - mousePosition.y //to top
            ) / (Screen.height / 2f);
            var xDistancePercentage = 1f - Mathf.Min(
                mousePosition.x, //to left
                Screen.width - mousePosition.x //to right
            ) / (Screen.width / 2f);


            if (yDistancePercentage < config.EdgePercentageToPan && xDistancePercentage < config.EdgePercentageToPan ||
                yDistancePercentage > 1f || xDistancePercentage > 1f) // prevent panning when mouse is outside of windows
            {
                return 0;
            }

            var center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            var mouseDistanceFromCenter = Vector3.Distance(mousePosition, center);
            var distanceInPercentage = mouseDistanceFromCenter / center.magnitude;
            var thresholdDistanceInPercentage = center.magnitude * config.EdgePercentageToPan;
            return Easing.ExponentialEaseInOut(
                Mathf.Abs(distanceInPercentage - thresholdDistanceInPercentage / (1f - thresholdDistanceInPercentage)),
                config.SmoothFactor
            );
        }

        #endregion
    }
}
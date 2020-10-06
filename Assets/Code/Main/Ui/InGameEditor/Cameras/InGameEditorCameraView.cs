using System;
using UniRx;
using UnityEngine;
using UnityUtils;

namespace Main.Ui.InGameEditor.Cameras
{
    public class InGameEditorCameraView : MonoBehaviour
    {
        [SerializeField] private Transform mapTransform;
        [SerializeField] private InGameEditorCameraConfig config;
        [SerializeField] private InGameEditorCameraViewModelFactory viewModelFactory;

        private InGameEditorCameraViewModel _viewModel;
        private IDisposable _disposable;
        private float _upBound;
        private float _downBound;
        private float _leftBound;
        private float _rightBound;
        private float _accumulatedZoomingValue;
        private float _accumulatedZoomingDecelerationValue;
        private float _currentZoomingDirection;


        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create(mapTransform, config);

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
            Zooming();
        }

        #region Panning

        private void OnUpdateCameraPosition(Vector3 newPosition)
        {
            _viewModel.CameraLiveData.Value.transform.position = newPosition;
        }

        private void Panning() //consider adding panning with middle mouse button
        {
            var panningStrength = GetPanningStrength();
            if (FloatUtil.NearlyEqual(panningStrength, 0f))
            {
                return;
            }

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

        #region Zooming

        private void Zooming()
        {
            var zoomingStrength = GetZoomingStrength();
            if (FloatUtil.NearlyEqual(zoomingStrength, 0f))
            {
                return;
            }

            _viewModel.OnZooming(zoomingStrength, Time.deltaTime);
        }


        private float GetZoomingStrength()
        {
            var zoomInput = Input.GetAxis("Mouse ScrollWheel");
            var inputStrength = Mathf.Abs(zoomInput);
            if (!FloatUtil.NearlyEqual(zoomInput, 0f))
            {
                _currentZoomingDirection = Mathf.Sign(zoomInput) * (config.IsInvertedWheel ? -1f : 1f);
            }

            if (!FloatUtil.NearlyEqual(zoomInput, 0f))
            {
                //accelerating
                _accumulatedZoomingDecelerationValue = 0;
                _accumulatedZoomingValue += inputStrength;
                _accumulatedZoomingValue = Mathf.Clamp01(_accumulatedZoomingValue);
            }
            else if (!FloatUtil.NearlyEqual(_accumulatedZoomingValue, 0f))
            {
                //decelerating

                _accumulatedZoomingDecelerationValue += config.DecelerationSpeed * Time.deltaTime;
                _accumulatedZoomingDecelerationValue = Mathf.Clamp01(_accumulatedZoomingDecelerationValue);

                _accumulatedZoomingValue -= Easing.ExponentialEaseInOut(_accumulatedZoomingDecelerationValue, config.SmoothFactor);
                _accumulatedZoomingValue = Mathf.Clamp01(_accumulatedZoomingValue);
            }
            else
            {
                //all is set, do nothing
                _currentZoomingDirection = 0f;
                return 0;
            }

            return _currentZoomingDirection * Easing.ExponentialEaseInOut(_accumulatedZoomingValue, config.SmoothFactor);
        }

        #endregion
    }
}
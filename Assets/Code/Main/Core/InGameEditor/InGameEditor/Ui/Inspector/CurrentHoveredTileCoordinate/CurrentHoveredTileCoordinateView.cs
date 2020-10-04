using System;
using Maps;
using TMPro;
using UnityEngine;

namespace InGameEditor.Ui.Inspector.CurrentHoveredTileCoordinate
{
    public class CurrentHoveredTileCoordinateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordinateText;
        [SerializeField] private CurrentHoveredTileCoordinateViewModelFactory viewModelFactory;

        private CurrentHoveredTileCoordinateViewModel _viewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = _viewModel.CoordinateLiveData.Subscribe(UpdateCoordinateText);
        }

        private void UpdateCoordinateText(Coordinate? coordinate)
        {
            if (coordinate == null)
            {
                coordinateText.text = string.Empty;
                return;
            }

            coordinateText.text = coordinate.ToString();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
            _viewModel?.Dispose();
        }
    }
}
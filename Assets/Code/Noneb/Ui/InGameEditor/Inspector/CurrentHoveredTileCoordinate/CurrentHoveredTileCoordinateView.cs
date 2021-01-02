using System;
using Noneb.Core.Game.Coordinates;
using TMPro;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.Inspector.CurrentHoveredTileCoordinate
{
    public class CurrentHoveredTileCoordinateView : MonoBehaviour
    {
        private IDisposable _disposable;

        private CurrentHoveredTileCoordinateViewModel _viewModel;
        [SerializeField] private TextMeshProUGUI coordinateText;
        [SerializeField] private CurrentHoveredTileCoordinateViewModelFactory viewModelFactory;

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
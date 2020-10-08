using System.Collections.Generic;
using System.Linq;
using Main.Core.Game.Coordinate;
using Main.Core.Game.WorldConfigurations;
using Main.Ui.InGameEditor.WorldSpace.GridOverlay.CellOverlay;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.InGameEditor.WorldSpace.GridOverlay
{
    public class GridOverlayView : MonoBehaviour
    {
        [FormerlySerializedAs("viewModelProvider")] [SerializeField]
        private GridOverlayViewModelFactory viewModelFactory;

        [FormerlySerializedAs("cellOverlayViewProvider")] [SerializeField]
        private CellOverlayViewFactory cellOverlayViewFactory;

        [SerializeField] private CellOverlayViewModelFactory cellOverlayViewModelFactory;
        [SerializeField] private Transform gridTransform;

        private GridOverlayViewModel _viewModel;
        private List<CellOverlayViewModel> _cells;
        private CompositeDisposable _compositeDisposable;


        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create(gridTransform);
            _cells = new List<CellOverlayViewModel>();
            _compositeDisposable = new CompositeDisposable
            {
                _viewModel.CoordinateVisibilityLiveData.Subscribe(SetCoordinatesVisibility),
                _viewModel.GridVisibilityLiveData.Subscribe(SetGridVisibility),
                _viewModel.CellsCountLiveData.Subscribe(OnUpdateCellsCount),
                _viewModel.CellsPositionLiveData.Subscribe(OnUpdateCellsPosition),
                _viewModel.WorldConfigLiveData.Subscribe(OnUpdateWorldConfig),
                _viewModel.CoordinatesLiveData.Subscribe(OnUpdateCoordinates)
            };
        }

        private void SetCoordinatesVisibility(bool visibility)
        {
            _cells.ForEach(c => c.OnSetCoordinateVisibility(visibility));
        }

        private void SetGridVisibility(bool visibility)
        {
            _cells.ForEach(c => c.OnSetLineVisibility(visibility));
        }

        private void OnUpdateCellsCount(int requiredCellsCount)
        {
            var currentCellsCount = _cells.Count;

            //delete cells if there too many of them
            for (var i = requiredCellsCount; i < currentCellsCount; i++)
            {
                var toRemove = _cells.Last();
                _cells.Remove(toRemove);

                toRemove.OnReceivedDestructionInstruction();
            }

            //create cells if there are not enough
            for (var i = currentCellsCount; i < requiredCellsCount; i++)
            {
                var cellOverlayView = cellOverlayViewFactory.Create(gridTransform).component;
                var cellOverlayViewModel = cellOverlayViewModelFactory.Create(
                    _viewModel.CoordinateVisibilityLiveData.Value,
                    _viewModel.GridVisibilityLiveData.Value
                );

                cellOverlayView.Initialize(cellOverlayViewModel);
                _cells.Add(cellOverlayViewModel);
            }
        }

        private void OnUpdateCellsPosition(IReadOnlyList<Vector3> positions)
        {
            for (var i = 0; i < _cells.Count; i++) _cells[i].OnUpdatePosition(positions[i]);
        }

        private void OnUpdateWorldConfig(WorldConfig config)
        {
            foreach (var cell in _cells) cell.OnUpdateWorldConfig(config);
        }

        private void OnUpdateCoordinates(IReadOnlyList<Coordinate> coordinates)
        {
            for (var i = 0; i < _cells.Count; i++) _cells[i].OnUpdateCoordinate(coordinates[i]);
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
            _viewModel.Dispose();
        }
    }
}
using System.Collections.Generic;
using InGameEditor.WorldSpace.GridOverlay.CellOverlay;
using Maps;
using Maps.Services;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations;

namespace InGameEditor.WorldSpace.GridOverlay
{
    public class GridOverlayView : MonoBehaviour
    {
        [FormerlySerializedAs("viewModelProvider")] [SerializeField]
        private GridOverlayViewModelFactory viewModelFactory;

        [SerializeField] private CellOverlayViewProvider cellOverlayViewProvider;
        [SerializeField] private CellOverlayViewModelFactory cellOverlayViewModelFactory;

        [FormerlySerializedAs("tilesPositionProvider")] [SerializeField]
        private TilesPositionServiceProvider tilesPositionServiceProvider;

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
                _viewModel.GenerateCellsLiveData.Subscribe(GenerateCells),
                _viewModel.UpdateCellsLiveData.Subscribe(UpdateCellsSettings)
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

        private void GenerateCells(GenerateCellsParameter generateCellsParameter)
        {
            for (var i = 0; i < generateCellsParameter.TilesPositions.Count; i++)
            {
                var cellOverlayView = cellOverlayViewProvider.Provide(gridTransform).Component;
                var cellOverlayViewModel = cellOverlayViewModelFactory.Create(
                    generateCellsParameter.TilesPositions[i],
                    generateCellsParameter.WorldConfiguration,
                    _viewModel.CoordinateVisibilityLiveData.Value,
                    _viewModel.GridVisibilityLiveData.Value,
                    generateCellsParameter.Coordinates[i]
                );

                cellOverlayView.Initialize(cellOverlayViewModel);

                _cells.Add(cellOverlayViewModel);
            }
        }

        private void UpdateCellsSettings(UpdateCellsSettingsParameter updateCellsSettingsParameter)
        {
            var tilesPositions = updateCellsSettingsParameter.TilesPositions;
            var worldConfiguration = updateCellsSettingsParameter.WorldConfiguration;

            for (var i = 0; i < tilesPositions.Count; i++)
            {
                _cells[i].OnUpdatePosition(tilesPositions[i]);
                _cells[i].OnUpdateWorldConfiguration(worldConfiguration);
            }
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
        }

        public class GenerateCellsParameter
        {
            public GenerateCellsParameter(WorldConfig worldConfiguration,
                                          IReadOnlyList<Vector3> tilesPositions,
                                          IReadOnlyList<Coordinate> coordinates)
            {
                WorldConfiguration = worldConfiguration;
                TilesPositions = tilesPositions;
                Coordinates = coordinates;
            }

            public IReadOnlyList<Coordinate> Coordinates { get; }
            public IReadOnlyList<Vector3> TilesPositions { get; }
            public WorldConfig WorldConfiguration { get; }
        }

        public class UpdateCellsSettingsParameter
        {
            public UpdateCellsSettingsParameter(WorldConfig worldConfiguration, IReadOnlyList<Vector3> tilesPositions)
            {
                WorldConfiguration = worldConfiguration;
                TilesPositions = tilesPositions;
            }

            public IReadOnlyList<Vector3> TilesPositions { get; }
            public WorldConfig WorldConfiguration { get; }
        }
    }
}
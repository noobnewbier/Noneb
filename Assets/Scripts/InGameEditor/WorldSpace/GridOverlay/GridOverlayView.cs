using System;
using System.Collections.Generic;
using System.Linq;
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
                _viewModel.GridParameterLiveData.Subscribe(ShowGridCells),
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

        private void ShowGridCells(GenerateCellsParameter generateCellsParameter)
        {
            var requiredCellsCount = generateCellsParameter.TilesPositions.Count;
            var worldConfig = generateCellsParameter.WorldConfiguration;
            var tilePositions = generateCellsParameter.TilesPositions;
            var coordinates = generateCellsParameter.Coordinates;
            var currentCellsCount = _cells.Count;
            
            //update existing cells
            for (var i = 0; i < Math.Min(requiredCellsCount, currentCellsCount); i++)
            {
                UpdateExistingCellsSettings(worldConfig, tilePositions[i], coordinates[i], _cells[i]);
            }
            
            //delete cells if there too many of them
            for (var i = requiredCellsCount; i < currentCellsCount; i++)
            {
                var toDestroy = _cells.Last();
                _cells.Remove(toDestroy);
                
                toDestroy.OnReceivedDestructionInstruction();
            }
            
            //create cells if there are not enough
            for (var i = currentCellsCount; i < requiredCellsCount; i++)
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

        private static void UpdateExistingCellsSettings(WorldConfig worldConfig, Vector3 tilePosition, Coordinate coordinate, CellOverlayViewModel toUpdate)
        {
            toUpdate.OnUpdatePosition(tilePosition);
            toUpdate.OnUpdateWorldConfiguration(worldConfig);
            toUpdate.OnUpdateCoordinate(coordinate);
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
            _viewModel.Dispose();
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
    }
}
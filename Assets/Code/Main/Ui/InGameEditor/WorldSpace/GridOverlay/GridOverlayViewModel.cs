using System;
using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Core.Game.GameState.CurrentWorldConfig;
using Main.Core.Game.WorldConfigurations;
using Main.Ui.Game.Maps.TilesPosition;
using UniRx;
using UnityEngine;

namespace Main.Ui.InGameEditor.WorldSpace.GridOverlay
{
    public class GridOverlayViewModel : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable;

        public GridOverlayViewModel(ICoordinateService coordinateService,
                                    ICurrentWorldConfigRepository currentWorldConfigRepository,
                                    ICurrentMapConfigRepository currentMapConfigRepository,
                                    ITilesPositionService tilesPositionService,
                                    Transform centerTransform)
        {
            CoordinateVisibilityLiveData = new LiveData<bool>(true);
            GridVisibilityLiveData = new LiveData<bool>(true);
            CellsCountLiveData = new LiveData<int>();
            CellsPositionLiveData = new LiveData<IReadOnlyList<Vector3>>();
            WorldConfigLiveData = new LiveData<WorldConfig>();
            CoordinatesLiveData = new LiveData<IReadOnlyList<Coordinate>>();

            _compositeDisposable = new CompositeDisposable
            {
                currentMapConfigRepository.GetObservableStream()
                    .CombineLatest(
                        currentWorldConfigRepository.GetObservableStream(),
                        tilesPositionService.GetObservableStream(centerTransform.position.y),
                        (mapConfig, worldConfig, positions) => (mapConfig, worldConfig, positions)
                    )
                    .Subscribe(
                        tuple =>
                        {
                            var (mapConfig, worldConfig, positions) = tuple;
                            var coordinates = coordinateService.GetFlattenCoordinates(mapConfig);

                            UpdateCellsCountsWhenNeeded(coordinates.Count);
                            UpdateCellsPositionWhenNeeded(positions);
                            UpdateWorldConfigWhenNeeded(worldConfig);
                            UpdateCoordinatesWhenNeeded(coordinates);
                        }
                    )
            };
        }

        public ILiveData<bool> CoordinateVisibilityLiveData { get; }
        public ILiveData<bool> GridVisibilityLiveData { get; }
        public ILiveData<int> CellsCountLiveData { get; }
        public ILiveData<IReadOnlyList<Vector3>> CellsPositionLiveData { get; }
        public ILiveData<WorldConfig> WorldConfigLiveData { get; }
        public ILiveData<IReadOnlyList<Coordinate>> CoordinatesLiveData { get; }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void UpdateCellsCountsWhenNeeded(int newValue)
        {
            if (CellsCountLiveData.Value != newValue)
            {
                CellsCountLiveData.PostValue(newValue);
            }
        }

        private void UpdateWorldConfigWhenNeeded(WorldConfig newValue)
        {
            if (WorldConfigLiveData.Value != newValue)
            {
                WorldConfigLiveData.PostValue(newValue);
            }
        }

        private void UpdateCoordinatesWhenNeeded(IReadOnlyList<Coordinate> newValue)
        {
            var currentValue = CoordinatesLiveData.Value;
            var isLengthEqual = newValue.Count == currentValue?.Count;
            var needUpdate = false;

            if (isLengthEqual)
            {
                for (var i = 0; i < currentValue?.Count; i++)
                    if (currentValue[i] != newValue[i])
                    {
                        needUpdate = true;
                    }
            }
            else
            {
                needUpdate = true;
            }


            if (needUpdate)
            {
                CoordinatesLiveData.PostValue(newValue);
            }
        }

        private void UpdateCellsPositionWhenNeeded(IReadOnlyList<Vector3> newValue)
        {
            var currentValue = CellsPositionLiveData.Value;
            var isLengthEqual = newValue.Count == currentValue?.Count;
            var needUpdate = false;

            if (isLengthEqual)
            {
                for (var i = 0; i < currentValue?.Count; i++)
                    if (currentValue[i] != newValue[i])
                    {
                        needUpdate = true;
                    }
            }
            else
            {
                needUpdate = true;
            }


            if (needUpdate)
            {
                CellsPositionLiveData.PostValue(newValue);
            }
        }
    }
}
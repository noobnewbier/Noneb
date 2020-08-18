using System;
using Experiment.CrossPlatformLiveData;
using Maps.Repositories;
using Maps.Services;
using UniRx;
using UnityEngine;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridOverlay
{
    public class GridOverlayViewModel : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable;

        public GridOverlayViewModel(IGetCoordinateService coordinateService,
                                    ICurrentWorldConfigRepository currentWorldConfigRepository,
                                    ICurrentMapConfigRepository currentMapConfigRepository,
                                    ITilesPositionService tilesPositionService,
                                    Transform centerTransform)
        {
            UpdateCellsLiveData = new LiveData<GridOverlayView.UpdateCellsSettingsParameter>();
            CoordinateVisibilityLiveData = new LiveData<bool>();
            GridVisibilityLiveData = new LiveData<bool>();
            GenerateCellsLiveData = new LiveData<GridOverlayView.GenerateCellsParameter>();

            var tilesPositionObservable = tilesPositionService.GetObservableStream(centerTransform.position.y);
            _compositeDisposable = new CompositeDisposable
            {
                currentWorldConfigRepository.GetObservableStream()
                    .ZipLatest(tilesPositionObservable, (config, position) => (config, position))
                    .Subscribe(
                        // ReSharper disable once ImplicitlyCapturedClosure : It's fine, it's not that big and we are cleaning it up anyway
                        tuple =>
                        {
                            var (config, positions) = tuple;
                            UpdateCellsLiveData.PostValue(new GridOverlayView.UpdateCellsSettingsParameter(config, positions));
                        }
                    ),

                currentMapConfigRepository.GetObservableStream()
                    .ZipLatest(
                        currentWorldConfigRepository.GetObservableStream(),
                        tilesPositionObservable,
                        (mapConfig, worldConfig, positions) => (mapConfig, worldConfig, positions)
                    )
                    .Subscribe(
                        tuple =>
                        {
                            var (mapConfig, worldConfig, positions) = tuple;
                            var coordinates = coordinateService.GetFlattenCoordinates(mapConfig);
                            GenerateCellsLiveData.PostValue(new GridOverlayView.GenerateCellsParameter(worldConfig, positions, coordinates));
                        }
                    )
            };
        }

        public ILiveData<bool> CoordinateVisibilityLiveData { get; }
        public ILiveData<bool> GridVisibilityLiveData { get; }
        public ILiveData<GridOverlayView.GenerateCellsParameter> GenerateCellsLiveData { get; }
        public ILiveData<GridOverlayView.UpdateCellsSettingsParameter> UpdateCellsLiveData { get; }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
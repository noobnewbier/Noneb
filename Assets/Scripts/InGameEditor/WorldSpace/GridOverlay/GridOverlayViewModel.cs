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
            CoordinateVisibilityLiveData = new LiveData<bool>(true);
            GridVisibilityLiveData = new LiveData<bool>(true);
            GridParameterLiveData = new LiveData<GridOverlayView.GenerateCellsParameter>();

            var tilesPositionObservable = tilesPositionService.GetObservableStream(centerTransform.position.y);
            _compositeDisposable = new CompositeDisposable
            {
                currentMapConfigRepository.GetObservableStream()
                    .CombineLatest(
                        currentWorldConfigRepository.GetObservableStream(),
                        tilesPositionObservable,
                        (mapConfig, worldConfig, positions) => (mapConfig, worldConfig, positions)
                    )
                    .Subscribe(
                        tuple =>
                        {
                            var (mapConfig, worldConfig, positions) = tuple;
                            var coordinates = coordinateService.GetFlattenCoordinates(mapConfig);
                            GridParameterLiveData.PostValue(new GridOverlayView.GenerateCellsParameter(worldConfig, positions, coordinates));
                        }
                    )
            };
        }

        public ILiveData<bool> CoordinateVisibilityLiveData { get; }
        public ILiveData<bool> GridVisibilityLiveData { get; }
        public ILiveData<GridOverlayView.GenerateCellsParameter> GridParameterLiveData { get; }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
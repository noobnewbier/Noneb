using System;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using Maps;
using UniRx;

namespace InGameEditor.Ui.Inspector.CurrentHoveredTileCoordinate
{
    public class CurrentHoveredTileCoordinateViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public CurrentHoveredTileCoordinateViewModel(IInGameEditorCurrentlyHoveredTileHolderGetRepository repository)
        {
            CoordinateLiveData = new LiveData<Coordinate>();

            _disposable = repository.GetObservableStream()
                .Subscribe(
                    holder =>
                        CoordinateLiveData.PostValue(holder.Value.Coordinate)
                );
        }

        public LiveData<Coordinate> CoordinateLiveData { get; }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
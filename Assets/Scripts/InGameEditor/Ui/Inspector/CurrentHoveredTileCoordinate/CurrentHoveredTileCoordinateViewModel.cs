using System;
using Common.Ui.Repository.CurrentHoveredTileHolder;
using Experiment.CrossPlatformLiveData;
using Maps;
using UniRx;

namespace InGameEditor.Ui.Inspector.CurrentHoveredTileCoordinate
{
    public class CurrentHoveredTileCoordinateViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public CurrentHoveredTileCoordinateViewModel(ICurrentHoveredTileHolderGetRepository repository)
        {
            CoordinateLiveData = new LiveData<Coordinate?>();

            _disposable = repository.GetObservableStream()
                .Subscribe(
                    holder =>
                        CoordinateLiveData.PostValue(holder != null ? holder.Value.Coordinate : (Coordinate?) null)
                );
        }

        public LiveData<Coordinate?> CoordinateLiveData { get; }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
using System;
using Experiment.CrossPlatformLiveData;
using Main.Core.Game.Coordinate;
using Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder;
using UniRx;

namespace Main.Ui.InGameEditor.Inspector.CurrentHoveredTileCoordinate
{
    public class CurrentHoveredTileCoordinateViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public CurrentHoveredTileCoordinateViewModel(ICurrentHoveredTileHolderGetRepository repository)
        {
            CoordinateLiveData = new LiveData<Coordinate?>();

            _disposable = repository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
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
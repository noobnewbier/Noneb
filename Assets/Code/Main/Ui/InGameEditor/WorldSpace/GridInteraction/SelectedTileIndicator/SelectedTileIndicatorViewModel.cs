using System;
using Experiment.CrossPlatformLiveData;
using Main.Ui.Game.Tiles;
using Main.Ui.Game.UiState.CurrentSelectedTileHolder;
using UniRx;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    public class SelectedTileIndicatorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public SelectedTileIndicatorViewModel(ICurrentSelectedTileHolderGetRepository selectedTileHolderGetRepository)
        {
            CurrentlySelectedTileHolderLiveData = new LiveData<TileHolder>();

            _disposable = selectedTileHolderGetRepository
                .GetObservableStream()
                .Subscribe(CurrentlySelectedTileHolderLiveData.PostValue);
        }

        public LiveData<TileHolder> CurrentlySelectedTileHolderLiveData { get; }


        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
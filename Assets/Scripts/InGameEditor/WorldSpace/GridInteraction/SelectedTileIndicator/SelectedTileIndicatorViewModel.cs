using System;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder;
using Tiles.Holders;
using UniRx;

namespace InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    public class SelectedTileIndicatorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public SelectedTileIndicatorViewModel(IInGameEditorCurrentSelectedTileHolderGetRepository selectedTileHolderGetRepository)
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
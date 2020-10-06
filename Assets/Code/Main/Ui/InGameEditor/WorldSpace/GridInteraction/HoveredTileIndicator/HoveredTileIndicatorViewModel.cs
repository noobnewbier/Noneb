using System;
using Experiment.CrossPlatformLiveData;
using Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder;
using Main.Ui.Game.Common.UiState.CurrentSelectedTileHolder;
using Main.Ui.Game.Tiles;
using UniRx;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    public class HoveredTileIndicatorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public HoveredTileIndicatorViewModel(ICurrentHoveredTileHolderGetRepository hoveredTileHolderGetRepository,
                                             ICurrentSelectedTileHolderGetRepository selectedTileHolderGetRepository)
        {
            CurrentlyHoveredTileHolderLiveData = new LiveData<TileHolder>();
            CurrentlySelectedTileHolderLiveData = new LiveData<TileHolder>();

            _disposable = new CompositeDisposable
            {
                hoveredTileHolderGetRepository
                    .GetObservableStream()
                    .Subscribe(CurrentlyHoveredTileHolderLiveData.PostValue),
                selectedTileHolderGetRepository
                    .GetObservableStream()
                    .Subscribe(CurrentlySelectedTileHolderLiveData.PostValue)
            };
        }

        public LiveData<TileHolder> CurrentlyHoveredTileHolderLiveData { get; }
        public LiveData<TileHolder> CurrentlySelectedTileHolderLiveData { get; }


        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
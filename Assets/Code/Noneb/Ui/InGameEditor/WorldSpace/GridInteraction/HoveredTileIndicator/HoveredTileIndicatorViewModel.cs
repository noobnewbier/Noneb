using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Ui.Game.Tiles;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using UniRx;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
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
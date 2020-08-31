using System;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder;
using Tiles.Holders;
using UniRx;

namespace InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    public class HoveredTileIndicatorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;

        public HoveredTileIndicatorViewModel(IInGameEditorCurrentHoveredTileHolderGetRepository hoveredTileHolderGetRepository,
                                             IInGameEditorCurrentSelectedTileHolderGetRepository selectedTileHolderGetRepository)
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
using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public class InspectorViewModel<T>: IDisposable where T: IInspectable
    {
        public ILiveData<T> TilePresetPaletteData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        private readonly IDisposable _disposable;

        public InspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository)
        {
            TilePresetPaletteData = new LiveData<T>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = currentInspectableGetRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(OnInspectableUpdate);
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (inspectable is T paletteData)
            {
                UpdateVisibility(true);
                UpdateInspectable(paletteData);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private void UpdateInspectable(T paletteData)
        {
            TilePresetPaletteData.PostValue(paletteData);
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
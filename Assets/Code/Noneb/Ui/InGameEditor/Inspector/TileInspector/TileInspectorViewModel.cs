using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    public class TileInspectorViewModel : IDisposable
    {
        public ILiveData<PaletteData<Preset<TileData>>> TilePresetPaletteData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        private readonly IDisposable _disposable;

        public TileInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository)
        {
            TilePresetPaletteData = new LiveData<PaletteData<Preset<TileData>>>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = currentInspectableGetRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(OnInspectableUpdate);
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (inspectable is PaletteData<Preset<TileData>> paletteData)
            {
                UpdateVisibility(true);
                UpdatePreset(paletteData);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private void UpdatePreset(PaletteData<Preset<TileData>> paletteData)
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
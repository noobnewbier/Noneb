using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public class InspectorViewModel<TItem, TData> : IDisposable
        where TData : BoardItemData
        where TItem : BoardItem<TData>
    {
        public ILiveData<TData> TypeTLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        private readonly IDisposable _disposable;
        private readonly IDisposable _coordinateDisposable;
        private Map _currentMap;

        public InspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository, IMapRepository mapRepository)
        {
            TypeTLiveData = new LiveData<TData>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = new CompositeDisposable
            {
                currentInspectableGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(OnInspectableUpdate),

                mapRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(
                        m => _currentMap = m
                    )
            };
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (TryGetTFromInspectable(inspectable, out var t))
            {
                UpdateVisibility(true);
                UpdateInspectable(t);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private bool TryGetTFromInspectable(IInspectable inspectable, out TData t)
        {
            switch (inspectable)
            {
                case InspectableCoordinate inspectableCoordinate:
                    var hasItem = _currentMap.TryGet<TItem>(inspectableCoordinate.Coordinate, out var item);
                    t = item?.Data;

                    return hasItem;
                case PaletteData<Preset<TData>> paletteData:
                    t = paletteData.Data.Data;
                    return true;
                default:
                    t = default;
                    return false;
            }
        }

        private void UpdateInspectable(TData tData)
        {
            TypeTLiveData.PostValue(tData);
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }

        public virtual void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
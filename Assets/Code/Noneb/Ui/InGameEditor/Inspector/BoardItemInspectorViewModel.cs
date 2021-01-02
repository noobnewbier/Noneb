﻿using System;
using Experiment.CrossPlatformLiveData;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public class BoardItemInspectorViewModel<TItem, TData> : IDisposable
        where TData : BoardItemData
        where TItem : BoardItem<TData>
    {
        private readonly IDisposable _coordinateDisposable;
        private readonly IDisposable _disposable;
        private IInspectable _currentlyInspected;
        private Map _currentMap;

        public BoardItemInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                           IMapGetService mapGetService,
                                           IMapEditingService mapEditingService)
        {
            TypeTLiveData = new LiveData<TData>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = new CompositeDisposable
            {
                currentInspectableGetRepository.GetObservableStream()
                    .SubscribeOn(NoobSchedulers.ThreadPool)
                    .ObserveOn(NoobSchedulers.MainThread)
                    .Subscribe(OnInspectableUpdate),

                mapGetService.GetObservableStream()
                    .SubscribeOn(NoobSchedulers.ThreadPool)
                    .ObserveOn(NoobSchedulers.MainThread)
                    .Subscribe(OnNewMapCreated),

                mapEditingService.ModifiedEventStream
                    .SubscribeOn(NoobSchedulers.ThreadPool)
                    .ObserveOn(NoobSchedulers.MainThread)
                    .Subscribe(_ => OnMapEdited())
            };
        }

        public ILiveData<TData> TypeTLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnNewMapCreated(Map m)
        {
            _currentMap = m;

            UpdateTData();
        }

        private void OnMapEdited()
        {
            if (_currentlyInspected is InspectableCoordinate) UpdateTData();
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            _currentlyInspected = inspectable;
            UpdateTData();
        }

        private void UpdateTData()
        {
            if (TryGetTFromInspectable(_currentlyInspected, out var t))
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
    }
}
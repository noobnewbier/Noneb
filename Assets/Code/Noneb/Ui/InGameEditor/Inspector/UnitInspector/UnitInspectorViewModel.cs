using System;
using Experiment.CrossPlatformLiveData;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Units;
using Noneb.Core.InGameEditor.Data;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    public class UnitInspectorViewModel : IDisposable
    {
        private readonly IDisposable _coordinateDisposable;
        private readonly IDisposable _disposable;
        private IInspectable _currentlyInspected;
        private Map _currentMap;

        public UnitInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                      IMapGetService mapGetService,
                                      IMapEditingService mapEditingService)
        {
            UnitDataLiveData = new LiveData<UnitData>();
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

        public ILiveData<UnitData> UnitDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnNewMapCreated(Map m)
        {
            _currentMap = m;

            UpdateUnitData();
        }

        private void OnMapEdited()
        {
            if (_currentlyInspected is InspectableCoordinate) UpdateUnitData();
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            _currentlyInspected = inspectable;
            UpdateUnitData();
        }

        private void UpdateUnitData()
        {
            if (TryGetUnitFromInspectable(_currentlyInspected, out var data))
            {
                UpdateVisibility(true);
                UnitDataLiveData.PostValue(data);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private bool TryGetUnitFromInspectable(IInspectable inspectable, out UnitData data)
        {
            switch (inspectable)
            {
                case InspectableCoordinate inspectableCoordinate:
                    if (_currentMap.TryGet<Unit>(inspectableCoordinate.Coordinate, out var unit))
                    {
                        data = unit?.Data;
                        return true;
                    }
                    else if (_currentMap.TryGet<Stronghold>(inspectableCoordinate.Coordinate, out var stronghold))
                    {
                        data = stronghold?.Data?.UnitData;
                        return true;
                    }

                    data = default;
                    return false;
                case PaletteData<Preset<UnitData>> paletteData:
                    data = paletteData.Data.Data;
                    return true;
                default:
                    data = default;
                    return false;
            }
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }
    }
}
using System;
using Experiment.CrossPlatformLiveData;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector.ConstructInspector
{
    public class ConstructInspectorViewModel : IDisposable
    {
        private readonly IDisposable _coordinateDisposable;
        private readonly IDisposable _disposable;
        private IInspectable _currentlyInspected;
        private Map _currentMap;

        public ConstructInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                           IMapGetService mapGetService,
                                           IMapEditingService mapEditingService)
        {
            ConstructDataLiveData = new LiveData<ConstructData>();
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

        public ILiveData<ConstructData> ConstructDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnNewMapCreated(Map m)
        {
            _currentMap = m;

            UpdateConstructData();
        }

        private void OnMapEdited()
        {
            if (_currentlyInspected is InspectableCoordinate) UpdateConstructData();
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            _currentlyInspected = inspectable;
            UpdateConstructData();
        }

        private void UpdateConstructData()
        {
            if (TryGetConstructFromInspectable(_currentlyInspected, out var data))
            {
                UpdateVisibility(true);
                ConstructDataLiveData.PostValue(data);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private bool TryGetConstructFromInspectable(IInspectable inspectable, out ConstructData data)
        {
            switch (inspectable)
            {
                case InspectableCoordinate inspectableCoordinate:
                    if (_currentMap.TryGet<Construct>(inspectableCoordinate.Coordinate, out var construct))
                    {
                        data = construct?.Data;
                        return true;
                    }
                    else if (_currentMap.TryGet<Stronghold>(inspectableCoordinate.Coordinate, out var stronghold))
                    {
                        data = stronghold?.Data?.ConstructData;
                        return true;
                    }

                    data = default;
                    return false;
                case PaletteData<Preset<ConstructData>> paletteData:
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
using System;
using Experiment.CrossPlatformLiveData;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    public class TileInspectorViewModel : IDisposable
    {
        private readonly IDisposable _coordinateDisposable;
        private readonly IDisposable _disposable;
        private IInspectable _currentlyInspected;
        private Map _currentMap;

        public TileInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                      IMapGetService mapGetService,
                                      IMapEditingService mapEditingService)
        {
            TileDataLiveData = new LiveData<TileData>();
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

        public ILiveData<TileData> TileDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnNewMapCreated(Map m)
        {
            _currentMap = m;

            UpdateTileData();
        }

        private void OnMapEdited()
        {
            if (_currentlyInspected is InspectableCoordinate) UpdateTileData();
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            _currentlyInspected = inspectable;
            UpdateTileData();
        }

        private void UpdateTileData()
        {
            if (TryGetTileFromInspectable(_currentlyInspected, out var data))
            {
                UpdateVisibility(true);
                TileDataLiveData.PostValue(data);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private bool TryGetTileFromInspectable(IInspectable inspectable, out TileData data)
        {
            switch (inspectable)
            {
                case InspectableCoordinate inspectableCoordinate:
                    var hasTile = _currentMap.TryGet<Tile>(inspectableCoordinate.Coordinate, out var tile);
                    data = tile?.Data;

                    return hasTile;
                case PaletteData<Preset<TileData>> paletteData:
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
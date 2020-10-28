using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    public class StrongholdInspectorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IDisposable _coordinateDisposable;
        private readonly IMapModificationService _mapModificationService;

        private Map _currentMap;
        private Coordinate _currentCoordinate;

        public StrongholdInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                            IMapRepository mapRepository,
                                            IMapModificationService mapModificationService)
        {
            _mapModificationService = mapModificationService;
            StrongholdDataLiveData = new LiveData<StrongholdData>();
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
                    ),
                
                _mapModificationService.ModifiedEventStream
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(_ => OnMapModified())
            };
        }

        public ILiveData<StrongholdData> StrongholdDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnMapModified()
        {
            if (IsCurrentlyVisible)
            {
                UpdateLiveData();
            }
        }

        private bool IsCurrentlyVisible => VisibilityLiveData.Value;

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (inspectable is InspectableCoordinate inspectableCoordinate)
            {
                _currentCoordinate = inspectableCoordinate.Coordinate;

                UpdateLiveData();
                UpdateVisibility(true);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private void UpdateLiveData()
        {
            var item = _currentMap.Get<Stronghold>(_currentCoordinate);
            StrongholdDataLiveData.PostValue(item?.Data);
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }

        public void SetIsStronghold(bool isStronghold)
        {
            if (isStronghold)
            {
                SetUpStronghold();
            }
            else
            {
                DestructStronghold();
            }
        }

        private void SetUpStronghold()
        {
            _mapModificationService.SetUpStronghold(_currentMap, _currentCoordinate)
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe();
        }

        private void DestructStronghold()
        {
            _mapModificationService.DestructStronghold(_currentMap, _currentCoordinate)
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe();
        }
    }
}
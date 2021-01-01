using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.InGameMessages;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.InGameEditor.Data;
using Noneb.Core.InGameEditor.LevelEdit;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    public class StrongholdInspectorViewModel : IDisposable
    {
        private readonly IDisposable _coordinateDisposable;
        private readonly IDisposable _disposable;
        private readonly IInGameMessageService _inGameMessageService;
        private readonly ILevelEditService _levelEditService;
        private Coordinate _currentCoordinate;

        private Map _currentMap;

        public StrongholdInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository,
                                            IMapGetService mapGetService,
                                            ILevelEditService levelEditService,
                                            IInGameMessageService inGameMessageService)
        {
            _levelEditService = levelEditService;
            _inGameMessageService = inGameMessageService;
            StrongholdDataLiveData = new LiveData<StrongholdData>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = new CompositeDisposable
            {
                currentInspectableGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(OnInspectableUpdate),

                mapGetService.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(
                        m => _currentMap = m
                    ),

                _levelEditService.ModifiedEventStream
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(_ => OnMapModified())
            };
        }

        public ILiveData<StrongholdData> StrongholdDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        private bool IsCurrentlyVisible => VisibilityLiveData.Value;

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnMapModified()
        {
            if (IsCurrentlyVisible)
            {
                UpdateStrongholdData();
                UpdateVisibility(IsVisibleOnCurrentCoordinate());
            }
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (inspectable is InspectableCoordinate inspectableCoordinate)
            {
                _currentCoordinate = inspectableCoordinate.Coordinate;

                UpdateStrongholdData();
                UpdateVisibility(IsVisibleOnCurrentCoordinate());
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private bool IsVisibleOnCurrentCoordinate() =>
            _currentMap.TryGet<Unit>(_currentCoordinate, out _) && _currentMap.TryGet<Construct>(_currentCoordinate, out _) ||
            _currentMap.TryGet<Stronghold>(_currentCoordinate, out _);

        private void UpdateStrongholdData()
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
                SetUpStronghold();
            else
                DestructStronghold();
        }

        private void SetUpStronghold()
        {
            _levelEditService.SetUpStronghold(_currentCoordinate)
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    _ => _inGameMessageService.PublishMessage("Stronghold Created"),
                    e => throw e
                );
        }

        private void DestructStronghold()
        {
            _levelEditService.DestructStronghold(_currentCoordinate)
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    _ => _inGameMessageService.PublishMessage("Stronghold Destructed"),
                    e => throw e
                );
        }
    }
}
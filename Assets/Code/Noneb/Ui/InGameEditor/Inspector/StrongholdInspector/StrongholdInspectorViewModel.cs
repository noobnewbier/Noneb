using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    public class StrongholdInspectorViewModel : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IDisposable _coordinateDisposable;
        private Map _currentMap;

        public StrongholdInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository, IMapRepository mapRepository)
        {
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
                    )
            };
        }

        public ILiveData<StrongholdData> StrongholdDataLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (inspectable is InspectableCoordinate inspectableCoordinate)
            {
                var item = _currentMap.Get<Stronghold>(inspectableCoordinate.Coordinate);

                StrongholdDataLiveData.PostValue(item?.Data);

                UpdateVisibility(true);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }
    }
}
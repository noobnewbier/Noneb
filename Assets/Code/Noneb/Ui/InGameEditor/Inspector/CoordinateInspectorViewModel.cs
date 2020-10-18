using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public class CoordinateInspectorViewModel<TItem, TData> : InspectorViewModelBase<TData>
        where TItem : BoardItem<TData>
        where TData : BoardItemData
    {
        private Map _currentMap;

        private readonly IDisposable _coordinateDisposable;

        public CoordinateInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository, IMapRepository mapRepository) : base(
            currentInspectableGetRepository
        )
        {
            _coordinateDisposable = mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    m => _currentMap = m
                );
        }

        protected override bool TryGetTFromInspectable(IInspectable inspectable, out TData t)
        {
            if (inspectable is InspectableCoordinate inspectableCoordinate)
            {
                var hasItem = _currentMap.TryGet<TItem>(inspectableCoordinate.Coordinate, out var item);
                t = item?.Data;

                return hasItem;
            }

            t = default;
            return false;
        }

        public override void Dispose()
        {
            base.Dispose();
            _coordinateDisposable?.Dispose();
        }
    }
}
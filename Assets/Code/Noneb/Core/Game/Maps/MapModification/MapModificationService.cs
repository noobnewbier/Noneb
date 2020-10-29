using System;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.Strongholds;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Core.Game.Maps.MapModification
{
    public interface IMapModificationService
    {
        IObservable<UniRx.Unit> ModifiedEventStream { get; }
        IObservable<UniRx.Unit> SetUpStronghold(Map map, Coordinate coordinate);
        IObservable<UniRx.Unit> DestructStronghold(Map map, Coordinate coordinate);
    }

    public class MapModificationService : IMapModificationService
    {
        public MapModificationService()
        {
            ModifiedEventStream = new Subject<UniRx.Unit>();
        }

        public IObservable<UniRx.Unit> ModifiedEventStream { get; }

        public IObservable<UniRx.Unit> SetUpStronghold(Map map, Coordinate coordinate)
        {
            return Observable.Create<UniRx.Unit>(
                observer =>
                {
                    var unit = map.Get<Unit>(coordinate);
                    var construct = map.Get<Construct>(coordinate);

                    map.Set(coordinate, new Stronghold(StrongholdData.Create(construct.Data, unit.Data), coordinate));
                    map.Set<Unit>(coordinate, null);
                    map.Set<Construct>(coordinate, null);
                    
                    observer.OnNext(UniRx.Unit.Default);
                    observer.OnCompleted();

                    PublishModifiedEvent();

                    return Disposable.Empty;
                }
            );
        }

        private void PublishModifiedEvent()
        {
            ModifiedEventStream.Publish(UniRx.Unit.Default);
        }

        public IObservable<UniRx.Unit> DestructStronghold(Map map, Coordinate coordinate)
        {
            return Observable.Create<UniRx.Unit>(
                observer =>
                {
                    var stronghold = map.Get<Stronghold>(coordinate);

                    var unit = new Unit(stronghold.Data.UnitData, coordinate);
                    var construct = new Construct(stronghold.Data.ConstructData, coordinate);

                    map.Set(coordinate, unit);
                    map.Set(coordinate, construct);
                    map.Set<Stronghold>(coordinate, null);
                    
                    observer.OnNext(UniRx.Unit.Default);
                    observer.OnCompleted();
                    
                    PublishModifiedEvent();

                    return Disposable.Empty;
                }
            );
        }
    }
}
using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameState.BoardItems;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Core.Game.GameState.Maps
{
    public interface IMapRepository : IDataGetRepository<Map>, IDisposable
    {
    }

    public class MapRepository : IMapRepository
    {
        private readonly IDisposable _disposable;
        private readonly ReplaySubject<Map> _mapSubject;
        private IObservable<Map> _mapSingle;

        public MapRepository(IMapConfigRepository mapConfigRepository,
                             IBoardItemsGetRepository<Tile> tilesRepository,
                             IBoardItemsGetRepository<Unit> unitsRepository,
                             IBoardItemsRepository<Construct> constructsRepository,
                             IBoardItemsGetRepository<Stronghold> strongholdsRepository)
        {
            _mapSingle = Observable.Throw<Map>(new InvalidOperationException($"Value is not set yet for {GetType().Name}"));
            _mapSubject = new ReplaySubject<Map>(1);

            _disposable = mapConfigRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .ZipLatest(
                    tilesRepository.GetObservableStream(),
                    unitsRepository.GetObservableStream(),
                    constructsRepository.GetObservableStream(),
                    strongholdsRepository.GetObservableStream(),
                    (config, tiles, units, constructs, strongholds) => (config, tiles, units, constructs, strongholds)
                )
                .Where(tuple => tuple.config != null)
                .Subscribe(
                    tuple =>
                    {
                        var (config, tiles, units, constructs, strongholds) = tuple;
                        var map = new Map(tiles, units, constructs, strongholds, config);
                        
                        _mapSubject.OnNext(map);
                        _mapSingle = Observable.Return(map);
                    }
                );
        }

        public IObservable<Map> GetObservableStream()
        {
            return _mapSubject;
        }

        public IObservable<Map> GetMostRecent()
        {
            return _mapSingle;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
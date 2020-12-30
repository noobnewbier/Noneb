using System;
using System.Collections.Generic;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameState.BoardItems;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Core.Game.GameState.Maps
{
    public interface IMapGetService : IDisposable
    {
        IObservable<Map> GetObservableStream();
        IObservable<Map> GetMostRecent();
    }

    //probably a bad idea to have the map only update when both items and config has an update(ZipLatest), might want to use CombineLatest instead
    public class MapGetService : IMapGetService
    {
        private readonly IDisposable _disposable;
        private readonly ReplaySubject<Map> _mapSubject;
        private IObservable<Map> _mapSingle;

        private Map _previousMap;

        //we cannot use replay subject here - it kills our test
        public MapGetService(IMapConfigRepository mapConfigRepository,
                             IBoardItemsGetRepository<Tile> tilesRepository,
                             IBoardItemsGetRepository<Unit> unitsRepository,
                             IBoardItemsGetRepository<Construct> constructsRepository,
                             IBoardItemsGetRepository<Stronghold> strongholdsRepository)
        {
            _mapSingle = Observable.Throw<Map>(new InvalidOperationException($"Value is not set yet for {GetType().Name}"));
            _mapSubject = new ReplaySubject<Map>(1);

            var groupedItemsObservableStream = CreateGroupedItemsObservableStream(
                tilesRepository,
                unitsRepository,
                constructsRepository,
                strongholdsRepository
            );
            
            _disposable = mapConfigRepository.GetObservableStream()
                .CombineLatest(
                    groupedItemsObservableStream,
                    (config, itemGroup) => (config, itemGroup.tiles, itemGroup.units, itemGroup.constructs, itemGroup.strongholds)
                )
                .SubscribeOn(NoobSchedulers.ThreadPool)
                .ObserveOn(NoobSchedulers.MainThread)
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

        private static IObservable<(
                IReadOnlyList<Tile>tiles,
                IReadOnlyList<Unit> units,
                IReadOnlyList<Construct> constructs,
                IReadOnlyList<Stronghold>strongholds)>
            CreateGroupedItemsObservableStream(
                IBoardItemsGetRepository<Tile> tilesRepository,
                IBoardItemsGetRepository<Unit> unitsRepository,
                IBoardItemsGetRepository<Construct> constructsRepository,
                IBoardItemsGetRepository<Stronghold> strongholdsRepository
            )
        {
            return tilesRepository.GetObservableStream().ZipLatest(
                unitsRepository.GetObservableStream(),
                constructsRepository.GetObservableStream(),
                strongholdsRepository.GetObservableStream(),
                (tiles, units, constructs, strongholds) => (tiles, units, constructs, strongholds)
            );
        }
    }
}
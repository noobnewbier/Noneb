using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameState.BoardItems;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Core.Game.GameState.Maps
{
    public interface IMapRepository : IDataGetRepository<Map>
    {
    }

    public class MapRepository : IMapRepository
    {
        private readonly IMapConfigRepository _mapConfigRepository;
        private readonly IBoardItemsGetRepository<Tile> _tilesRepository;
        private readonly IBoardItemsGetRepository<Unit> _unitsRepository;
        private readonly IBoardItemsRepository<Construct> _constructsRepository;
        private readonly IBoardItemsGetRepository<Stronghold> _strongholdsRepository;

        public MapRepository(IMapConfigRepository mapConfigRepository,
                             IBoardItemsGetRepository<Tile> tilesRepository,
                             IBoardItemsGetRepository<Unit> unitsRepository,
                             IBoardItemsRepository<Construct> constructsRepository,
                             IBoardItemsGetRepository<Stronghold> strongholdsRepository)
        {
            _mapConfigRepository = mapConfigRepository;
            _tilesRepository = tilesRepository;
            _unitsRepository = unitsRepository;
            _constructsRepository = constructsRepository;
            _strongholdsRepository = strongholdsRepository;
        }

        public IObservable<Map> GetObservableStream()
        {
            return _mapConfigRepository.GetObservableStream()
                .ZipLatest(
                    _tilesRepository.GetObservableStream(),
                    _unitsRepository.GetObservableStream(),
                    _constructsRepository.GetObservableStream(),
                    _strongholdsRepository.GetObservableStream(),
                    (config, tiles, units, constructs, strongholds) => (config, tiles, units, constructs, strongholds)
                )
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Map(tuple.tiles, tuple.units, tuple.constructs, tuple.strongholds, tuple.config));
        }

        public IObservable<Map> GetMostRecent()
        {
            return _mapConfigRepository.GetMostRecent()
                .ZipLatest(
                    _tilesRepository.GetMostRecent(),
                    _unitsRepository.GetMostRecent(),
                    _constructsRepository.GetMostRecent(),
                    _strongholdsRepository.GetMostRecent(),
                    (config, tiles, units, constructs, strongholds) => (config, tiles, units, constructs, strongholds)
                )
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Map(tuple.tiles, tuple.units, tuple.constructs, tuple.strongholds, tuple.config));
        }
    }
}
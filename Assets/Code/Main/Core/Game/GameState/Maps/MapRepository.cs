using System;
using Main.Core.Game.Common;
using Main.Core.Game.GameState.BoardItems;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Core.Game.Maps;
using Main.Core.Game.Tiles;
using UniRx;

namespace Main.Core.Game.GameState.Maps
{
    public interface IMapRepository : IDataGetRepository<Map>
    {
    }

    public class MapRepository : IMapRepository
    {
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;
        private readonly IBoardItemsGetRepository<Tile> _tilesRepository;

        public MapRepository(ICurrentMapConfigRepository currentMapConfigRepository,
                             IBoardItemsGetRepository<Tile> tilesRepository)
        {
            _currentMapConfigRepository = currentMapConfigRepository;
            _tilesRepository = tilesRepository;
        }

        public IObservable<Map> GetObservableStream()
        {
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tilesRepository.GetObservableStream(), (config, tiles) => (config, tiles))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Map(tuple.tiles, tuple.config));
        }

        public IObservable<Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tilesRepository.GetObservableStream(), (config, tiles) => (config, tiles))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Map(tuple.tiles, tuple.config));
        }
    }
}
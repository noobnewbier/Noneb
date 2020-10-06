using System;
using Main.Core.Game.Common;
using Main.Core.Game.GameEnvironments.BoardItems;
using Main.Core.Game.Maps.CurrentMapConfig;
using Main.Core.Game.Tiles;
using UniRx;

namespace Main.Core.Game.Maps
{
    public interface IMapRepository : IDataGetRepository<Maps.Map>
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

        public IObservable<Maps.Map> GetObservableStream()
        {
            return _currentMapConfigRepository.GetObservableStream()
                .ZipLatest(_tilesRepository.GetObservableStream(), (config, tiles) => (config, tiles))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tiles, tuple.config));
        }

        public IObservable<Maps.Map> GetMostRecent()
        {
            return _currentMapConfigRepository.GetMostRecent()
                .Where(m => m != null)
                .ZipLatest(_tilesRepository.GetObservableStream(), (config, tiles) => (config, tiles))
                .Where(tuple => tuple.config != null)
                .Select(tuple => new Maps.Map(tuple.tiles, tuple.config));
        }
    }
}
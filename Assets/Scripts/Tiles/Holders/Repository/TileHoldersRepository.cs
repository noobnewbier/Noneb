using System.Collections.Generic;
using Maps;

namespace Tiles.Holders.Repository
{
    public interface ITileHoldersRepository
    {
        TileHolder Get(Coordinate axialCoordinate);
        IEnumerable<TileHolder> GetAllFlatten();
    }

    public class TileHoldersRepository : ITileHoldersRepository
    {
        private readonly int _mapXSize;
        private readonly int _mapZSize;
        private readonly TileHolder[,] _tileRepresentations;

        public TileHoldersRepository(IReadOnlyList<TileHolder> tiles, int mapXSize, int mapZSize)
        {
            _mapXSize = mapXSize;
            _mapZSize = mapZSize;
            _tileRepresentations = new TileHolder[_mapXSize + _mapZSize / 2, _mapZSize];
            for (var i = 0; i < _mapZSize; i++)
            for (var j = 0; j < _mapXSize; j++)
                _tileRepresentations[j + i % 2 + i / 2, i] = tiles[i * _mapXSize + j];
        }

        public TileHolder Get(Coordinate axialCoordinate)
        {
            return _tileRepresentations[axialCoordinate.X, axialCoordinate.Z];
        }

        public IEnumerable<TileHolder> GetAllFlatten()
        {
            var toReturn = new TileHolder[_mapXSize * _mapZSize];

            for (var i = 0; i < _mapZSize; i++)
            for (var j = 0; j < _mapXSize; j++)
                toReturn[i * _mapXSize + j] = _tileRepresentations[j + i % 2 + i / 2, i];

            return toReturn;
        }
    }
}
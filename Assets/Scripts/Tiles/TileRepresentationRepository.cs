using System.Collections.Generic;

namespace Tiles
{
    public interface ITileRepresentationRepository
    {
        TileRepresentation Get(Coordinate axialCoordinate);
        IEnumerable<TileRepresentation> GetAllFlatten();
    }

    public class TileRepresentationRepository : ITileRepresentationRepository
    {
        private readonly int _mapXSize;
        private readonly int _mapZSize;
        private readonly TileRepresentation[,] _tileRepresentations;

        public TileRepresentationRepository(IReadOnlyList<TileRepresentation> tiles, int mapXSize, int mapZSize)
        {
            _mapXSize = mapXSize;
            _mapZSize = mapZSize;
            _tileRepresentations = new TileRepresentation[_mapXSize + _mapZSize / 2, _mapZSize];
            for (var i = 0; i < _mapZSize; i++)
            for (var j = 0; j < _mapXSize; j++)
                _tileRepresentations[j + i % 2 + i / 2, i] = tiles[i * _mapXSize + j];
        }

        public TileRepresentation Get(Coordinate axialCoordinate)
        {
            return _tileRepresentations[axialCoordinate.X, axialCoordinate.Z];
        }

        public IEnumerable<TileRepresentation> GetAllFlatten()
        {
            var toReturn = new TileRepresentation[_mapXSize * _mapZSize];

            for (var i = 0; i < _mapZSize; i++)
            for (var j = 0; j < _mapXSize; j++)
                toReturn[i * _mapXSize + j] = _tileRepresentations[j + i % 2 + i / 2, i];

            return toReturn;
        }
    }
}
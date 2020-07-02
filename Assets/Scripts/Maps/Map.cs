using System;
using System.Collections.Generic;
using Tiles;

namespace Maps
{
    public class Map
    {
        private readonly Tile[,] _grid;

        public Map(IReadOnlyList<Tile> tiles, int mapXSize, int mapZSize)
        {
            _grid = new Tile[mapXSize + mapZSize / 2, mapZSize];
            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
                _grid[j + i % 2 + i / 2, i] = tiles[i * mapXSize + j];
        }

        public IReadOnlyDictionary<HexDirection, Tile> GetNeighbours(Coordinate axialCoordinate)
        {
            //makes more sense to hard code, it is possible to use a loop but it is even harder to read
            var minusX = axialCoordinate + HexDirection.MinusX;
            var plusX = axialCoordinate + HexDirection.PlusX;
            var minusXMinusZ = axialCoordinate + HexDirection.MinusXMinusZ;
            var minusZ = axialCoordinate + HexDirection.MinusZ;
            var plusZ = axialCoordinate + HexDirection.PlusZ;
            var plusXPlusZ = axialCoordinate + HexDirection.PlusXPlusZ;

            var toReturn = new Dictionary<HexDirection, Tile>
            {
                [HexDirection.MinusX] = GetTileWithDefault(minusX.X, minusX.Z),
                [HexDirection.PlusX] = GetTileWithDefault(plusX.X, plusX.Z),
                [HexDirection.MinusXMinusZ] = GetTileWithDefault(minusXMinusZ.X, minusXMinusZ.Z),
                [HexDirection.MinusZ] = GetTileWithDefault(minusZ.X, minusZ.Z),
                [HexDirection.PlusZ] = GetTileWithDefault(plusZ.X, plusZ.Z),
                [HexDirection.PlusXPlusZ] = GetTileWithDefault(plusXPlusZ.X, plusXPlusZ.Z)
            };

            return toReturn;
        }

        public Tile Get(Coordinate axialCoordinate)
        {
            return _grid[axialCoordinate.X, axialCoordinate.Z];
        }

        //return null when out of bounds
        private Tile GetTileWithDefault(int x, int z)
        {
            try
            {
                return _grid[x, z];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }
}
using System.Collections.Generic;
using Tiles;

namespace Maps
{
    public class Map
    {
        private readonly Tile[,] _grid;

        public Map(Tile[,] grid)
        {
            _grid = grid;
        }
        
        public IReadOnlyDictionary<HexDirection, Tile> GetNeighbours(Coordinate coordinate)
        {
            var x = coordinate.X;
            var z = coordinate.Z;
            //makes more sense to hard code, it is possible to use a loop but it is even harder to read
            var toReturn = new Dictionary<HexDirection, Tile>
            {
                [HexDirection.MinusX] = _grid[x, z],
                [HexDirection.PlusX] = _grid[x + 1, z],
                [HexDirection.MinusXPlusZ] = _grid[x - 1, z + 1],
                [HexDirection.MinusZ] = _grid[x, z - 1],
                [HexDirection.PlusZ] = _grid[x, z + 1],
                [HexDirection.PlusXMinusZ] = _grid[x + 1, z - 1]
            };

            return toReturn;
        }
    }
}
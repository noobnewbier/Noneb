using System;
using System.Collections.Generic;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.Tiles;

namespace Noneb.Core.Game.Maps
{
    /*
     * A map holds tile according to axial coordinate, it has nothing to do with any other stuffs
     * the storage is in axial coordinate with grid(jagged array) for easy access.
     */
    public class Map
    {
        private readonly Tile[,] _grid;

        public Map(IReadOnlyList<Tile> tiles, MapConfig mapConfig)
        {
            var map2DArrayWidth = mapConfig.GetMap2DArrayWidth();
            var map2dArrayHeight = mapConfig.GetMap2DArrayHeight();
            var map2DActualWidth = mapConfig.GetMap2DActualWidth();
            var map2dActualHeight = mapConfig.GetMap2DActualHeight();

            _grid = new Tile[map2DArrayWidth, map2dArrayHeight];
            for (var i = 0; i < map2dActualHeight; i++)
            for (var j = 0; j < map2DActualWidth; j++)
                _grid[j + i % 2 + i / 2, i] = tiles[i * map2DActualWidth + j];
        }

        public IReadOnlyDictionary<HexDirection, Tile> GetNeighbours(Coordinate axialCoordinate)
        {
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

        public Tile Get(Coordinate axialCoordinate) => _grid[axialCoordinate.X, axialCoordinate.Z];

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
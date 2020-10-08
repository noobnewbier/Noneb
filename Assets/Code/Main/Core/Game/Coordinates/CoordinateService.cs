using System.Collections.Generic;
using Main.Core.Game.Maps;

namespace Main.Core.Game.Coordinates
{
    public interface ICoordinateService
    {
        Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z);
        Coordinate GetCoordinateFromFlattenArrayIndex(int index, MapConfig mapConfig);
        int GetFlattenArrayIndexFromAxialCoordinate(int x, int z, MapConfig config);

        IReadOnlyList<Coordinate> GetFlattenCoordinates(MapConfig mapConfig);
    }

    public class CoordinateService : ICoordinateService
    {
        public Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z)
        {
            var axialX = x + z % 2 + z / 2;
            var axialZ = z;
            return new Coordinate(axialX, axialZ);
        }

        public Coordinate GetCoordinateFromFlattenArrayIndex(int index, MapConfig config)
        {
            var nestedArrayZ = index / config.GetMap2DActualHeight();
            var nestedArrayX = index - nestedArrayZ * config.GetMap2DActualWidth();

            return GetAxialCoordinateFromNestedArrayIndex(nestedArrayX, nestedArrayZ);
        }

        public int GetFlattenArrayIndexFromAxialCoordinate(int x, int z, MapConfig config) => z * config.GetMap2DActualWidth() + x - z % 2 - z / 2;

        public IReadOnlyList<Coordinate> GetFlattenCoordinates(MapConfig mapConfig)
        {
            var toReturn = new List<Coordinate>();

            for (var i = 0; i < mapConfig.GetMap2DActualHeight(); i++)
            for (var j = 0; j < mapConfig.GetMap2DActualWidth(); j++)
                toReturn.Add(GetAxialCoordinateFromNestedArrayIndex(j, i));

            return toReturn;
        }
    }
}
using System.Collections.Generic;

namespace Maps.Services
{
    public interface IGetCoordinateService
    {
        Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z);
        Coordinate GetCoordinateFromFlattenArrayIndex(int index, MapConfig mapConfig);

        IReadOnlyList<Coordinate> GetFlattenCoordinates(MapConfig mapConfig);
    }

    public class GetCoordinateService : IGetCoordinateService
    {
        public Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z)
        {
            var axialX = x + z % 2 + z / 2;
            var axialZ = z;
            return new Coordinate(axialX, axialZ);
        }

        public Coordinate GetCoordinateFromFlattenArrayIndex(int index, MapConfig config)
        {
            var nestedArrayZ = index / config.ZSize;
            var nestedArrayX = index - nestedArrayZ * config.XSize;

            return GetAxialCoordinateFromNestedArrayIndex(nestedArrayX, nestedArrayZ);
        }

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
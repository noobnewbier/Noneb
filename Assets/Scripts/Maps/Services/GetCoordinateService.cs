using System.Collections.Generic;

namespace Maps.Services
{
    public interface IGetCoordinateService
    {
        Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z);

        IReadOnlyList<Coordinate> GetFlattenCoordinates(MapConfig mapConfiguration);
    }

    public class GetCoordinateService : IGetCoordinateService
    {
        public Coordinate GetAxialCoordinateFromNestedArrayIndex(int x, int z)
        {
            var axialX = x + z % 2 + z / 2;
            var axialZ = z;
            return new Coordinate(axialX, axialZ);
        }

        public IReadOnlyList<Coordinate> GetFlattenCoordinates(MapConfig mapConfiguration)
        {
            var toReturn = new List<Coordinate>();

            for (var i = 0; i < mapConfiguration.GetMap2DActualHeight(); i++)
            for (var j = 0; j < mapConfiguration.GetMap2DActualWidth(); j++)
                toReturn.Add(GetAxialCoordinateFromNestedArrayIndex(j, i));

            return toReturn;
        }
    }
}
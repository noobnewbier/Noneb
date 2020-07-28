namespace Maps.Services
{
    public interface IGetCoordinateService
    {
        Coordinate GetAxialCoordinate(int x, int z);
    }

    public class GetCoordinateService : IGetCoordinateService
    {
        public Coordinate GetAxialCoordinate(int x, int z)
        {
            var axialX = x + z % 2 + z / 2;
            var axialZ = z;
            return new Coordinate(axialX, axialZ);
        }
    }
}
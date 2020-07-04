using Maps;

namespace Constructs
{
    public class Construct
    {
        public Coordinate Coordinate { get; }
        public ConstructData ConstructData { get; }

        public Construct(Coordinate coordinate, ConstructData constructData)
        {
            Coordinate = coordinate;
            ConstructData = constructData;
        }
    }
}
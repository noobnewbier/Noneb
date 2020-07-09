using Constructs;
using Maps;
using Units;

namespace Strongholds
{
    public class Stronghold
    {
        public Unit Unit { get; }
        public Construct Construct { get; }
        public Coordinate Coordinate { get; }

        public Stronghold(Unit unit, Construct construct, Coordinate coordinate)
        {
            Unit = unit;
            Construct = construct;
            Coordinate = coordinate;
        }
    }
}
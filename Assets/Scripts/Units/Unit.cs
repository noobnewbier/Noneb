using Maps;

namespace Units
{
    public class Unit
    {
        public Unit(UnitData unitData, Coordinate coordinate)
        {
            UnitData = unitData;
            Coordinate = coordinate;
        }

        public UnitData UnitData { get; }
        public Coordinate Coordinate { get; private set; }

        public void MoveTo(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}
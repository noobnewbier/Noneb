using Common.BoardItems;
using Common.TagInterface;
using Maps;
using Units.Data;

namespace Units
{
    public class Unit : BoardItem<UnitData>, IOnTile
    {
        public Unit(UnitData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
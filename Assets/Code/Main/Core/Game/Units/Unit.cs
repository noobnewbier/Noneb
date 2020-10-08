using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;

namespace Main.Core.Game.Units
{
    public class Unit : BoardItem<UnitData>, IOnTile
    {
        public Unit(UnitData data, Coordinate.Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
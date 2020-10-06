using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;
using Main.Core.Game.Maps.Coordinate;

namespace Main.Core.Game.Units
{
    public class Unit : BoardItem<UnitData>, IOnTile
    {
        public Unit(UnitData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
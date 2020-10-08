using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;

namespace Main.Core.Game.Constructs
{
    public class Construct : BoardItem<ConstructData>, IOnTile
    {
        public Construct(ConstructData data, Coordinate.Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
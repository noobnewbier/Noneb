using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;
using Main.Core.Game.Coordinates;

namespace Main.Core.Game.Constructs
{
    public class Construct : BoardItem<ConstructData>, IOnTile
    {
        public Construct(ConstructData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
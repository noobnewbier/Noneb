using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;
using Main.Core.Game.Constructs.Data;
using Main.Core.Game.Maps.Coordinate;

namespace Main.Core.Game.Constructs
{
    public class Construct : BoardItem<ConstructData>, IOnTile
    {
        public Construct(ConstructData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
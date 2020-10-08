using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;

namespace Main.Core.Game.Strongholds
{
    public class Stronghold : BoardItem<StrongholdData>, IOnTile
    {
        public Stronghold(StrongholdData data, Coordinate.Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
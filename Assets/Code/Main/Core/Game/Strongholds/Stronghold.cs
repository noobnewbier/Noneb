using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.TagInterface;
using Main.Core.Game.Maps.Coordinate;

namespace Main.Core.Game.Strongholds
{
    public class Stronghold : BoardItem<StrongholdData>, IOnTile
    {
        public Stronghold(StrongholdData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
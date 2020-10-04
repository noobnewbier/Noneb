using Common.BoardItems;
using Common.TagInterface;
using Maps;

namespace Strongholds
{
    public class Stronghold : BoardItem<StrongholdData>, IOnTile
    {
        public Stronghold(StrongholdData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
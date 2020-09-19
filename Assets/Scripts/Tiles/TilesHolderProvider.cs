using Common.BoardItems;
using Common.Constants;
using Tiles.Holders;

namespace Tiles
{
    public class TilesHolderProvider : BoardItemsHolderProvider<TileHolder>
    {
        protected override string HolderTag => ObjectTags.TileHolder;
    }
}
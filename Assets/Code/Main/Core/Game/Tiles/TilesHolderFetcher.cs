using Common.BoardItems;
using Common.Constants;
using Tiles.Holders;

namespace Tiles
{
    public class TilesHolderFetcher : BoardItemsHolderFetcher<TileHolder>
    {
        protected override string HolderTag => ObjectTags.TileHolder;
    }
}
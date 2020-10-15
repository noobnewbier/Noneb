using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.Tiles
{
    public class TilesHolderFetcher : BoardItemsHolderFetcher<TileHolder>
    {
        protected override string HolderTag => ObjectTags.TileHolder;
    }
}
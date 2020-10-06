using Main.Core.Game.Common.Constants;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.Tiles
{
    public class TilesHolderFetcher : BoardItemsHolderFetcher<TileHolder>
    {
        protected override string HolderTag => ObjectTags.TileHolder;
    }
}
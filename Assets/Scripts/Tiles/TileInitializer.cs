using Common.Constants;
using Common.Holders;
using Tiles.Holders;

namespace Tiles
{
    public class TileInitializer : HoldersInitializer<Tile, TileHolder>
    {
        protected override string RepresentationTag => ObjectTags.TileRepresentation;
    }
}
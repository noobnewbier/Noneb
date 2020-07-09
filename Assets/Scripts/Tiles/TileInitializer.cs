using Common.Constants;
using Common.Representations;
using Tiles.Representation;

namespace Tiles
{
    public class TileInitializer : RepresentationInitializer<Tile, TileRepresentation>
    {
        protected override string RepresentationTag => ObjectTags.TileRepresentation;
    }
}
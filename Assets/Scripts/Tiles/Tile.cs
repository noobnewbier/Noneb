using BoardItems;
using Maps;
using Tiles.Data;

namespace Tiles
{
    public class Tile : BoardItem<TileData>
    {
        public Tile(TileData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
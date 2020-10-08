using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Coordinates;

namespace Main.Core.Game.Tiles
{
    public class Tile : BoardItem<TileData>
    {
        public Tile(TileData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
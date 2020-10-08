using Main.Core.Game.Common.BoardItems;

namespace Main.Core.Game.Tiles
{
    public class Tile : BoardItem<TileData>
    {
        public Tile(TileData data, Coordinate.Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
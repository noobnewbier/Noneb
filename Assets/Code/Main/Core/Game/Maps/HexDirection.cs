namespace Main.Core.Game.Maps
{
    public class HexDirection
    {
        public static readonly HexDirection PlusX = new HexDirection(new Coordinate.Coordinate(1, 0));
        public static readonly HexDirection MinusX = new HexDirection(new Coordinate.Coordinate(-1, 0));
        public static readonly HexDirection PlusZ = new HexDirection(new Coordinate.Coordinate(0, 1));
        public static readonly HexDirection MinusZ = new HexDirection(new Coordinate.Coordinate(0, -1));
        public static readonly HexDirection PlusXPlusZ = new HexDirection(new Coordinate.Coordinate(1, 1));
        public static readonly HexDirection MinusXMinusZ = new HexDirection(new Coordinate.Coordinate(-1, -1));

        private HexDirection(Coordinate.Coordinate direction)
        {
            Direction = direction;
        }

        private Coordinate.Coordinate Direction { get; }

        public static Coordinate.Coordinate operator +(Coordinate.Coordinate a, HexDirection d) => a + d.Direction;
    }
}
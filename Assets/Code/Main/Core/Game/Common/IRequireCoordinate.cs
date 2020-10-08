using Main.Core.Game.Coordinates;

namespace Main.Core.Game.Common
{
    public interface IRequireCoordinate
    {
        Coordinate Coordinate { set; }
    }
}
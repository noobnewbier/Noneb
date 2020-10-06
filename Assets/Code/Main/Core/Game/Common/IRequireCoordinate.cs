using Main.Core.Game.Maps.Coordinate;

namespace Main.Core.Game.Common
{
    public interface IRequireCoordinate
    {
        Coordinate Coordinate { set; }
    }
}
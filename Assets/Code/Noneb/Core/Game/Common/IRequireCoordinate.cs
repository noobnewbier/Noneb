using Noneb.Core.Game.Coordinates;

namespace Noneb.Core.Game.Common
{
    public interface IRequireCoordinate
    {
        Coordinate Coordinate { set; }
    }
}
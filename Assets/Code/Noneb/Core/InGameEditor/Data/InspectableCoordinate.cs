using Noneb.Core.Game.Coordinates;
using Noneb.Core.InGameEditor.Common;

namespace Noneb.Core.InGameEditor.Data
{
    public class InspectableCoordinate : IInspectable
    {
        public InspectableCoordinate(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }
    }
}
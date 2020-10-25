using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.Coordinates;

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
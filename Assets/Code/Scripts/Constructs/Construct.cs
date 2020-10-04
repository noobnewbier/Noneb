using Common.BoardItems;
using Common.TagInterface;
using Constructs.Data;
using Maps;

namespace Constructs
{
    public class Construct : BoardItem<ConstructData>, IOnTile
    {
        public Construct(ConstructData data, Coordinate coordinate) : base(data, coordinate)
        {
        }
    }
}
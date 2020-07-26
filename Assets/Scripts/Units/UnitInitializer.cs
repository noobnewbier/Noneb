using Common.Constants;
using Common.Holders;
using Units.Holders;

namespace Units
{
    public class UnitInitializer : HoldersInitializer<Unit, UnitHolder>
    {
        protected override string RepresentationTag => ObjectTags.UnitRepresentation;
    }
}
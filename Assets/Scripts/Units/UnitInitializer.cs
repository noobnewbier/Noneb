using Common.Constants;
using Common.Representations;
using Units.Representation;

namespace Units
{
    public class UnitInitializer : RepresentationInitializer<Unit, UnitRepresentation>
    {
        protected override string RepresentationTag => ObjectTags.UnitRepresentation;
    }
}
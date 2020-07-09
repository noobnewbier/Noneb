using Common.Constants;
using Common.Representations;

namespace Constructs
{
    public class ConstructInitializer : RepresentationInitializer<Construct, ConstructRepresentation>
    {
        protected override string RepresentationTag => ObjectTags.ConstructRepresentation;
    }
}
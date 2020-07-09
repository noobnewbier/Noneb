using Common.Constants;
using Common.Representations;

namespace Strongholds
{
    public class StrongholdInitializer : RepresentationInitializer<Stronghold, StrongholdRepresentation>
    {
        protected override string RepresentationTag => ObjectTags.StrongholdRepresentation;
    }
}
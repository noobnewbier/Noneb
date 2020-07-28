using Common.Constants;
using Common.Holders;

namespace Constructs
{
    public class ConstructInitializer : HoldersInitializer<Construct, ConstructHolder>
    {
        protected override string HolderTag => ObjectTags.ConstructHolder;
    }
}
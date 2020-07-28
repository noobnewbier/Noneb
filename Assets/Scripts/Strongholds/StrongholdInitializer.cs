using Common.Constants;
using Common.Holders;

namespace Strongholds
{
    public class StrongholdInitializer : HoldersInitializer<Stronghold, StrongholdHolder>
    {
        protected override string HolderTag => ObjectTags.StrongholdHolder;
    }
}
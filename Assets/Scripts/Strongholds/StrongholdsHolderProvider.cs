using Common.BoardItems;
using Common.Constants;

namespace Strongholds
{
    public class StrongholdsHolderProvider : BoardItemsHolderProvider<StrongholdHolder>
    {
        protected override string HolderTag => ObjectTags.StrongholdHolder;
    }
}
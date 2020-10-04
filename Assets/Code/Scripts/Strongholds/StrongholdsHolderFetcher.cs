using Common.BoardItems;
using Common.Constants;

namespace Strongholds
{
    public class StrongholdsHolderFetcher : BoardItemsHolderFetcher<StrongholdHolder>
    {
        protected override string HolderTag => ObjectTags.StrongholdHolder;
    }
}
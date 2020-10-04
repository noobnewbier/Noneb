using Common.BoardItems;
using Common.Constants;

namespace Constructs
{
    public class ConstructsHolderFetcher : BoardItemsHolderFetcher<ConstructHolder>
    {
        protected override string HolderTag => ObjectTags.ConstructHolder;
    }
}
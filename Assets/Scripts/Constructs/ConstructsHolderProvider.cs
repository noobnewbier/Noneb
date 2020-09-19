using Common.BoardItems;
using Common.Constants;

namespace Constructs
{
    public class ConstructsHolderProvider : BoardItemsHolderProvider<ConstructHolder>
    {
        protected override string HolderTag => ObjectTags.ConstructHolder;
    }
}
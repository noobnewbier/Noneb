using Common.BoardItems;
using Common.Constants;
using Units.Holders;

namespace Units
{
    public class UnitsHolderFetcher : BoardItemsHolderFetcher<UnitHolder>
    {
        protected override string HolderTag => ObjectTags.UnitHolder;
    }
}
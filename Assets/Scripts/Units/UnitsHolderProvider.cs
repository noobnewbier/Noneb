using Common.BoardItems;
using Common.Constants;
using Units.Holders;

namespace Units
{
    public class UnitsHolderProvider : BoardItemsHolderProvider<UnitHolder>
    {
        protected override string HolderTag => ObjectTags.UnitHolder;
    }
}
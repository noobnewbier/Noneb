using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.Units
{
    public class UnitsHolderFetcher : BoardItemsHolderFetcher<UnitHolder>
    {
        protected override string HolderTag => ObjectTags.UnitHolder;
    }
}
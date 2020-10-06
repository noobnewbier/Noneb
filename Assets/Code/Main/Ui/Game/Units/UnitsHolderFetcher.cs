using Main.Core.Game.Common.Constants;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.Units
{
    public class UnitsHolderFetcher : BoardItemsHolderFetcher<UnitHolder>
    {
        protected override string HolderTag => ObjectTags.UnitHolder;
    }
}
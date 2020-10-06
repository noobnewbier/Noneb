using Main.Core.Game.Common.Constants;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.Constructs
{
    public class ConstructsHolderFetcher : BoardItemsHolderFetcher<ConstructHolder>
    {
        protected override string HolderTag => ObjectTags.ConstructHolder;
    }
}
using Main.Core.Game.Common.Constants;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.Strongholds
{
    public class StrongholdsHolderFetcher : BoardItemsHolderFetcher<StrongholdHolder>
    {
        protected override string HolderTag => ObjectTags.StrongholdHolder;
    }
}
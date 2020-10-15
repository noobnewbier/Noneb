using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.Strongholds
{
    public class StrongholdsHolderFetcher : BoardItemsHolderFetcher<StrongholdHolder>
    {
        protected override string HolderTag => ObjectTags.StrongholdHolder;
    }
}
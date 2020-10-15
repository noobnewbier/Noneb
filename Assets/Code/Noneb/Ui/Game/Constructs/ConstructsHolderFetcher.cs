using Noneb.Core.Game.Common.Constants;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.Constructs
{
    public class ConstructsHolderFetcher : BoardItemsHolderFetcher<ConstructHolder>
    {
        protected override string HolderTag => ObjectTags.ConstructHolder;
    }
}
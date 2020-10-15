using Noneb.Core.Game.Common;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher
{
    public class BoardItemsHolderFetcherRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderFetcher<THolder>
    {
    }
}
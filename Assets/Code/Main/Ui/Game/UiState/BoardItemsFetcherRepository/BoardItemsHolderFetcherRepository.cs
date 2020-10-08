using Main.Core.Game.Common;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.UiState.BoardItemsFetcherRepository
{
    public class BoardItemsHolderFetcherRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderFetcher<THolder>
    {
    }
}
using Main.Core.Game.Common;
using Main.Ui.Game.Common.Holders;

//todo: your namespace fucked up
namespace Main.Ui.Game.UiState.BoardItemsFetcher
{
    public class BoardItemsHolderFetcherRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderFetcher<THolder>
    {
    }
}
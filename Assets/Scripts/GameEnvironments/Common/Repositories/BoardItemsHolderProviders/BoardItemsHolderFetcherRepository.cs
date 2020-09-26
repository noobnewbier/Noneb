using Common;
using Common.BoardItems;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders
{
    //todo : link this with the actual BoardItemsHolderRepository
    public class BoardItemsHolderFetcherRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderFetcher<THolder>
    {
    }
}
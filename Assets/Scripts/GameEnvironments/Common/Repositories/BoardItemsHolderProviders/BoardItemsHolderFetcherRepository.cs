using Common;
using Common.BoardItems;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders
{
    public class BoardItemsHolderFetcherRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderFetcher<THolder>
    {
    }
}
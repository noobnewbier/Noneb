using Common;
using Common.BoardItems;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider
{
    public class BoardItemsHolderProviderRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderProvider<THolder>
    {
    }
}
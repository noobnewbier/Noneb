using System.Collections.Generic;
using Common;
using Common.Holders;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder
{
    public interface IBoardItemsHolderRepository<THolder> : IDataRepository<IReadOnlyList<THolder>> where THolder : IBoardItemHolder
    {
    }

    public class BoardItemsHolderRepository<THolder> : DataRepository<IReadOnlyList<THolder>>, IBoardItemsHolderRepository<THolder>
        where THolder : IBoardItemHolder
    {
    }
}
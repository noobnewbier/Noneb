using System.Collections.Generic;
using Common;
using Common.Holders;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder
{
    public class BoardItemsHolderRepository<THolder> : DataRepository<IReadOnlyList<THolder>>
        where THolder : IBoardItemHolder
    {
    }
}
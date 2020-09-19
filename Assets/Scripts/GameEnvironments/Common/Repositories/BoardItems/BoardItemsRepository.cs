using System.Collections.Generic;
using Common;
using Common.BoardItems;

namespace GameEnvironments.Common.Repositories.BoardItems
{
    public interface IBoardItemsGetRepository<out T> : IDataGetRepository<IReadOnlyList<T>> where T : BoardItem
    {
    }

    public interface IBoardItemsSetRepository<in T> : IDataSetRepository<IReadOnlyList<T>> where T : BoardItem
    {
    }

    public interface IBoardItemsRepository<T> : IBoardItemsGetRepository<T>, IBoardItemsSetRepository<T> where T : BoardItem
    {
    }

    public class BoardItemsRepository<T> : DataRepository<IReadOnlyList<T>>, IBoardItemsRepository<T> where T : BoardItem
    {
    }
}
using System.Collections.Generic;
using Main.Core.Game.Common;
using Main.Core.Game.Common.BoardItems;

namespace Main.Core.Game.GameEnvironments.BoardItems
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
using Maps;

namespace Common.BoardItems
{
    public abstract class BoardItem
    {
    }

    public abstract class BoardItem<TData> : BoardItem where TData : IBoardItemData
    {
        public TData Data { get; }
        public Coordinate Coordinate { get; }

        protected BoardItem(TData data, Coordinate coordinate)
        {
            Data = data;
            Coordinate = coordinate;
        }
        
    }
}
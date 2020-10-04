using Maps;

namespace Common.BoardItems
{
    public abstract class BoardItem
    {
        protected BoardItem(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }
    }

    public abstract class BoardItem<TData> : BoardItem where TData : BoardItemData
    {
        protected BoardItem(TData data, Coordinate coordinate) : base(coordinate)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}
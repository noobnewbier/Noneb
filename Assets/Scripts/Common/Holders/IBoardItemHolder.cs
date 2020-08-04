using Common.BoardItems;

namespace Common.Holders
{
    //Do we ACTUALLY need this, in theory we can probably get away without it considering all the holders are doing is showing gizmos
    public interface IBoardItemHolder<T>  where T : BoardItem
    {
        T Value { get; }
        void Initialize(T t);
    }
}
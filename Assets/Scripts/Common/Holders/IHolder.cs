namespace Common.Holders
{
    //1. Does `T` actually need to be bound as a class tho....
    //2. Do we ACTUALLY need this, in theory we can probably get away without it considering all the holders are doing is showing gizmos
    public interface IHolder<T> where T : class
    {
        T Value { get; }
        void Initialize(T t);
    }
}
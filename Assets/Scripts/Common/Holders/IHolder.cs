namespace Common.Holders
{
    //does it actually need to be bound as a class tho....
    public interface IHolder<in T> where T : class
    {
        void Initialize(T t);
    }
}
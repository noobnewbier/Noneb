namespace Common.Representations
{
    //does it actually need to be bound as a class tho....
    public interface IRepresentation<in T> where T : class
    {
        void Initialize(T t);
    }
}
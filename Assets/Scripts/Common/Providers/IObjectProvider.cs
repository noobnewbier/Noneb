namespace Common.Providers
{
    public interface IObjectProvider<out T>
    {
        T Provide();
    }
}
namespace Common
{
    public interface IObjectProvider<out T>
    {
        T Provide();
    }
}
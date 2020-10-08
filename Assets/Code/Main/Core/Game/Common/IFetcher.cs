namespace Main.Core.Game.Common
{
    public interface IFetcher<out T>
    {
        T Fetch();
    }
}
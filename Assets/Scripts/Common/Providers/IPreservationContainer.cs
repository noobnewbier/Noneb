namespace Common.Providers
{
    public interface IPreservationContainer<out T> where T : class
    {
        T GetPreservation();
    }
}
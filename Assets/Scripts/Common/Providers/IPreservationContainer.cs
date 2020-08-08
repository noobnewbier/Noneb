using GameEnvironments.Common.Data.GameEnvironments;

namespace Common.Providers
{
    /// <summary>
    /// Do we actually need this... At the moment we can completely store EVERYTHING with the <see cref="GameEnvironment"/>
    /// But this does make it easier to visualize craps...
    /// </summary>
    public interface IPreservationContainer<out T> where T : class
    {
        T GetPreservation();
    }
}
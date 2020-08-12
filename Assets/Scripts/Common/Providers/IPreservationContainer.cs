using System;
using GameEnvironments.Common.Data;

namespace Common.Providers
{
    /// <summary>
    /// Do we actually need this... At the moment we can completely store EVERYTHING with the <see cref="GameEnvironment" />
    /// But this does make it easier to visualize craps...
    /// </summary>
    [Obsolete("This is no longer use atm, pending to be removed once confirmed is useless")]
    public interface IPreservationContainer<out T> where T : class
    {
        T GetPreservation();
    }
}
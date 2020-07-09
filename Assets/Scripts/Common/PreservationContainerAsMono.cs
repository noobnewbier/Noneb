using Common.Providers;
using UnityEngine;

namespace Common
{
    //restricted as a class to allow easy null checking
    public abstract class PreservationContainerAsMono<T> : MonoBehaviour, IPreservationContainer<T> where T : class
    {
        private T _cache;
        protected abstract T CreateFromPreservation();

        //can potentially listen to event and refresh when signal is fired.
        public T GetPreservation()
        {
            return _cache ?? (_cache = CreateFromPreservation());
        }
    }
}
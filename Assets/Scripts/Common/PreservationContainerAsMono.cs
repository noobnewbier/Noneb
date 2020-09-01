using Common.Providers;
using UnityEngine;

namespace Common
{
    //restricted as a class to allow easy null checking
    public abstract class PreservationContainerAsMono<T> : MonoBehaviour, IPreservationContainer<T> where T : class
    {
        private T _cache;

        //can potentially listen to event and refresh when signal is fired.
        public T GetPreservation()
        {
            return _cache ?? (_cache = CreateFromPreservation());
        }

        protected abstract T CreateFromPreservation();
    }
}
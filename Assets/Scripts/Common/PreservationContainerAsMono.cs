using UnityEngine;

namespace Common
{
    //restricted as a class to allow easy null checking
    public abstract class PreservationContainerAsMono<T> : MonoBehaviour, IPreservationContainer<T> where T:class  
    {
        private T _cache;

        protected abstract T CreateFromPreservation();

        public T GetPreservation()
        {
            return _cache ?? (_cache = CreateFromPreservation());
        }
    }
}
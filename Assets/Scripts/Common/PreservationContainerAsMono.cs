using UnityEngine;

namespace Common
{
    //restricted as a class to allow easy null checking
    public abstract class PreservationContainerAsMono<T> : MonoBehaviour, IPreservationContainer<T> where T : class
    {
        private T _cache;
        protected abstract T CreateFromPreservation();

        public T GetPreservation()
        {
            if (Application.isPlaying)
            {
                return _cache ?? (_cache = CreateFromPreservation());
            }

            return CreateFromPreservation();
        }
    }
}
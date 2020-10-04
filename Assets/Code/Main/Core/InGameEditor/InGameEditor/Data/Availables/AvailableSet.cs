using UnityEngine;

namespace InGameEditor.Data.Availables
{
    public abstract class AvailableSet<T> : ScriptableObject
    {
        [SerializeField] private T[] set;

        public T[] Set => set;
    }
}
using UnityEngine;

namespace InGameEditor.Data.Availables
{
    public abstract class AvailableSet<T> : ScriptableObject
    {
        [SerializeField] private T[] _set;

        public T[] Set => _set;
    }
}
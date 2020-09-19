using UnityEngine;

namespace InGameEditor.Data.Availables
{
    //todo: consider renaming it to a more generic thing, it's essentially just a wrapper of an array atm
    public abstract class AvailableSet<T> : ScriptableObject
    {
        [SerializeField] private T[] set;

        public T[] Set => set;
    }
}
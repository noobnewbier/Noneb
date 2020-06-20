using UnityEngine;

namespace Common
{
    public abstract class ObjectProvider<T> : ScriptableObject
    {
        public abstract T Provide();
    }
}
using UnityEngine;

namespace Common
{
    public abstract class ScriptableObjectProvider<T> : ScriptableObject, IObjectProvider<T>
    {
        public abstract T Provide();
    }
}
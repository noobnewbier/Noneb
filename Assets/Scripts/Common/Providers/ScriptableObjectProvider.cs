using UnityEngine;

namespace Common.Providers
{
    public abstract class ScriptableObjectProvider<T> : ScriptableObject, IObjectProvider<T>
    {
        public abstract T Provide();
    }
}
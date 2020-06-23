using UnityEngine;

namespace Common
{
    public abstract class MonoObjectProvider<T> : MonoBehaviour, IObjectProvider<T>
    {
        public abstract T Provide();
    }
}
using UnityEngine;

namespace Common.Providers
{
    public interface IObjectProvider<out T>
    {
        T Provide();
    }

    public interface IGameObjectProvider : IObjectProvider<GameObject>
    {
        GameObject Provide(Transform parentTransform, bool instantiateInWorldSpace = true);
    }

    public interface IGameObjectAndComponentProvider<T> : IObjectProvider<(T component, GameObject gameObject)> where T : Component
    {
        (T component, GameObject gameObject) Provide(Transform parentTransform, bool instantiateInWorldSpace = true);
    }
}
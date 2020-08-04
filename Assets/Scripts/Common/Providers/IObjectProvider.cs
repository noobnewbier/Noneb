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

    public interface IGameObjectAndComponentProvider<T> : IObjectProvider<GameObjectAndComponent<T>> where T : Component
    {
        GameObjectAndComponent<T> Provide(Transform parentTransform, bool instantiateInWorldSpace = true);
    }
}
using UnityEngine;

namespace Common.Providers
{
    public abstract class ScriptableObjectProvider<T> : ScriptableObject, IObjectProvider<T>
    {
        public abstract T Provide();
    }

    public abstract class ScriptableGameObjectAndComponentProvider<T> : ScriptableObjectProvider<GameObjectAndComponent<T>>,
                                                                        IGameObjectAndComponentProvider<T> where T : Component
    {
        public abstract GameObjectAndComponent<T> Provide(Transform parentTransform, bool instantiateInWorldSpace);
    }
}
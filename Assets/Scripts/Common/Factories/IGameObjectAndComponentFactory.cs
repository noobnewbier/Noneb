using Common.Factories;
using UnityEngine;

namespace Common.Providers
{
    public interface IGameObjectAndComponentFactory<T> : IFactory<(T component, GameObject gameObject)> where T : Component
    {
        (T component, GameObject gameObject) Create(Transform parentTransform, bool instantiateInWorldSpace = true);
    }
}
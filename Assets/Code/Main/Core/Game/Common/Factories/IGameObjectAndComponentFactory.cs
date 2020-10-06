using UnityEngine;

namespace Main.Core.Game.Common.Factories
{
    public interface IGameObjectAndComponentFactory<T> : IFactory<(T component, GameObject gameObject)> where T : Component
    {
        (T component, GameObject gameObject) Create(Transform parentTransform, bool instantiateInWorldSpace = true);
    }
}
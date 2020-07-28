using UnityEngine;

namespace Common
{
    public class GameObjectAndComponent<T> where T : Component
    {
        public GameObjectAndComponent(GameObject gameObject, T component)
        {
            GameObject = gameObject;
            Component = component;
        }

        public GameObject GameObject { get; }
        public T Component { get; }
    }
}
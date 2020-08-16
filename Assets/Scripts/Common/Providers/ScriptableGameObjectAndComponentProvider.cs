using UnityEngine;

namespace Common.Providers
{
    public abstract class ScriptableGameObjectAndComponentProvider<T> : ScriptableObjectProvider<GameObjectAndComponent<T>>,
                                                                        IGameObjectAndComponentProvider<T> where T : Component
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<T> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<T>();
            return new GameObjectAndComponent<T>(go, component);
        }

        public GameObjectAndComponent<T> Provide(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<T>();
            return new GameObjectAndComponent<T>(go, component);
        }
    }
}
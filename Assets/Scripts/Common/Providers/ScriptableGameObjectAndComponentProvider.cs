using System;
using UnityEngine;

namespace Common.Providers
{
    public abstract class ScriptableGameObjectAndComponentProvider<T> : ScriptableObjectProvider<(T component, GameObject gameObject)>,
                                                                        IGameObjectAndComponentProvider<T> where T : Component
    {
        [SerializeField] private GameObject prefab;

        public override (T component, GameObject gameObject) Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<T>();
            return (component, go);
        }

        public (T component, GameObject gameObject) Provide(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<T>();
            return (component, go);
        }

        private void OnValidate()
        {
            if (prefab.GetComponent<T>() == null)
            {
                throw new ArgumentException($"{prefab.name} does not have the required component");
            }
        }
    }
}
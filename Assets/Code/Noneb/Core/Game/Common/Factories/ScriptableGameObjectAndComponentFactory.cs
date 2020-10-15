using System;
using UnityEngine;

namespace Noneb.Core.Game.Common.Factories
{
    public abstract class ScriptableGameObjectAndComponentFactory<T> : ScriptableObject,
                                                                       IGameObjectAndComponentFactory<T> where T : Component
    {
        [SerializeField] private GameObject prefab;

        public (T component, GameObject gameObject) Create()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<T>();
            return (component, go);
        }

        public (T component, GameObject gameObject) Create(Transform parentTransform, bool instantiateInWorldSpace = true)
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
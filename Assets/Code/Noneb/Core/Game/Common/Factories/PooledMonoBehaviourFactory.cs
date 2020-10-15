using System;
using UnityEngine;
using UnityUtils.Pooling;

namespace Noneb.Core.Game.Common.Factories
{
    public class PooledMonoBehaviourFactory<T> : ScriptableObject,
                                                 IGameObjectAndComponentFactory<T> where T : PooledMonoBehaviour
    {
        [SerializeField] private T prefab;


        public (T component, GameObject gameObject) Create(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            var go = prefab.GetPooledInstance();
            go.transform.SetParent(parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<T>();

            return (component, go);
        }

        public (T component, GameObject gameObject) Create()
        {
            var go = prefab.GetPooledInstance();
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
using System;
using UnityEngine;
using UnityUtils;

namespace Common.Providers
{
    public class PooledMonoBehaviourProvider<T> : ScriptableObjectProvider<(T component, GameObject gameObject)>,
                                                  IGameObjectAndComponentProvider<T> where T : PooledMonoBehaviour
    {
        [SerializeField] private T prefab;

        public override (T component, GameObject gameObject) Provide()
        {
            var go = prefab.GetPooledInstance();
            var component = go.GetComponent<T>();
            return (component, go);
        }

        public (T component, GameObject gameObject) Provide(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            var go = prefab.GetPooledInstance();
            go.transform.SetParent(parentTransform, instantiateInWorldSpace);
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
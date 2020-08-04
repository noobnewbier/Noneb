using Common;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Constructs
{
    [CreateAssetMenu(menuName = MenuName.Providers+"ConstructHolder", fileName = nameof(ConstructHolderProvider))]
    public class ConstructHolderProvider : ScriptableGameObjectAndComponentProvider<ConstructHolder>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<ConstructHolder> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<ConstructHolder>();
            return new GameObjectAndComponent<ConstructHolder>(go, component);
        }

        public override GameObjectAndComponent<ConstructHolder> Provide(Transform parentTransform, bool instantiateInWorldSpace)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<ConstructHolder>();
            return new GameObjectAndComponent<ConstructHolder>(go, component);
        }
    }
}
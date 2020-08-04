using Common;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Units.Holders
{
    [CreateAssetMenu(menuName = MenuName.Providers+"UnitHolder", fileName = nameof(UnitHolderProvider))]
    public class UnitHolderProvider : ScriptableGameObjectAndComponentProvider<UnitHolder>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<UnitHolder> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<UnitHolder>();
            return new GameObjectAndComponent<UnitHolder>(go, component);
        }

        public override GameObjectAndComponent<UnitHolder> Provide(Transform parentTransform, bool instantiateInWorldSpace)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<UnitHolder>();
            return new GameObjectAndComponent<UnitHolder>(go, component);
        }
    }
}
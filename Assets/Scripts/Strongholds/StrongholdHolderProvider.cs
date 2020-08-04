using Common;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Strongholds
{
    [CreateAssetMenu(menuName = MenuName.Providers+"StrongholdHolder", fileName = nameof(StrongholdHolderProvider))]
    public class StrongholdHolderProvider: ScriptableGameObjectAndComponentProvider<StrongholdHolder>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<StrongholdHolder> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<StrongholdHolder>();
            return new GameObjectAndComponent<StrongholdHolder>(go, component);
        }

        public override GameObjectAndComponent<StrongholdHolder> Provide(Transform parentTransform, bool instantiateInWorldSpace)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<StrongholdHolder>();
            return new GameObjectAndComponent<StrongholdHolder>(go, component);
        }
    }
}
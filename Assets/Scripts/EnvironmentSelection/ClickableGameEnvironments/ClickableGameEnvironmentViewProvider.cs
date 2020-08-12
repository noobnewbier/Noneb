using Common;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace EnvironmentSelection.ClickableGameEnvironments
{
    [CreateAssetMenu(fileName = nameof(ClickableGameEnvironmentViewProvider), menuName = MenuName.Providers + nameof(ClickableGameEnvironmentView))]
    public class ClickableGameEnvironmentViewProvider : ScriptableGameObjectAndComponentProvider<ClickableGameEnvironmentView>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<ClickableGameEnvironmentView> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<ClickableGameEnvironmentView>();
            return new GameObjectAndComponent<ClickableGameEnvironmentView>(go, component);
        }

        public override GameObjectAndComponent<ClickableGameEnvironmentView> Provide(Transform parentTransform, bool instantiateInWorldSpace)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<ClickableGameEnvironmentView>();
            return new GameObjectAndComponent<ClickableGameEnvironmentView>(go, component);
        }
    }
}
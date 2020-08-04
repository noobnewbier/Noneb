using Common;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Tiles.Holders
{
    [CreateAssetMenu(menuName = MenuName.Providers+"TileHolder", fileName = nameof(TileHolderProvider))]
    public class TileHolderProvider : ScriptableGameObjectAndComponentProvider<TileHolder>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<TileHolder> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<TileHolder>();
            return new GameObjectAndComponent<TileHolder>(go, component);
        }

        public override GameObjectAndComponent<TileHolder> Provide(Transform parentTransform, bool instantiateInWorldSpace)
        {
            var go = Instantiate(prefab, parentTransform, instantiateInWorldSpace);
            var component = go.GetComponent<TileHolder>();
            return new GameObjectAndComponent<TileHolder>(go, component);
        }
    }
}
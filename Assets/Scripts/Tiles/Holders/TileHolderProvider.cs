using Common;
using Common.Providers;
using UnityEngine;

namespace Tiles.Holders
{
    [CreateAssetMenu(menuName = "Providers/TileHolder", fileName = nameof(TileHolderProvider))]
    public class TileHolderProvider : ScriptableObjectProvider<GameObjectAndComponent<TileHolder>>
    {
        [SerializeField] private GameObject prefab;

        public override GameObjectAndComponent<TileHolder> Provide()
        {
            var go = Instantiate(prefab);
            var component = go.GetComponent<TileHolder>();
            return new GameObjectAndComponent<TileHolder>(go, component);
        }
    }
}
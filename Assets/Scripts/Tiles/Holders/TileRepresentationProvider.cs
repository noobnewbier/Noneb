using Common.Providers;
using UnityEngine;

namespace Tiles.Holders
{
    [CreateAssetMenu(menuName = "Providers/TileRepresentation", fileName = "TileRepresentationProvider")]
    public class TileRepresentationProvider : ScriptableObjectProvider<TileHolder>
    {
        [SerializeField] private GameObject prefab;

        public override TileHolder Provide()
        {
            return Instantiate(prefab).GetComponent<TileHolder>();
        }
    }
}
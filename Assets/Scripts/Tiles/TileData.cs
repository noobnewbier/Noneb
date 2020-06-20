using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Data/Tile", fileName = "TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private float weight;

        public float Weight => weight;
    }
}
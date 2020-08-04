using BoardItems;
using UnityEngine;

namespace Tiles.Data
{
    [CreateAssetMenu(menuName = "Data/Tile", fileName = "TileData")]
    public class TileData : ScriptableObject, IBoardItemData
    {
        [SerializeField] private float weight;

        public float Weight => weight;
    }
}
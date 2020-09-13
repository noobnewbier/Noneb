using Common.BoardItems;
using UnityEngine;

namespace Tiles.Data
{
    [CreateAssetMenu(menuName = "Data/Tile", fileName = "TileData")]
    public class TileData : BoardItemData
    {
        [SerializeField] private string tileName;
        [SerializeField] private float weight;

        public float Weight => weight;
        public override string DataName => tileName;
    }
}
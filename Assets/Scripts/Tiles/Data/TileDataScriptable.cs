using Common.BoardItems;
using UnityEngine;

namespace Tiles.Data
{
    [CreateAssetMenu(menuName = "Data/Tile", fileName = "TileData")]
    public class TileDataScriptable : BoardItemDataScriptable
    {
        [SerializeField] private string tileName;
        [SerializeField] private float weight;

        public float Weight => weight;

        public TileData ToData() => new TileData(Icon, tileName, this);
    }
}
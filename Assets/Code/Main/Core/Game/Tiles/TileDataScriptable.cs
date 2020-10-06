using Main.Core.Game.Common.BoardItems;
using UnityEngine;

namespace Main.Core.Game.Tiles
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
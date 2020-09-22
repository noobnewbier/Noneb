using Common.BoardItems;
using UnityEngine;

namespace Tiles.Data
{
    public class TileData : BoardItemData
    {
        public TileData(Sprite icon, string name, TileDataScriptable original) : base(icon, name)
        {
            Original = original;
        }

        public TileDataScriptable Original { get; }
        public float Weight => Original.Weight;
    }
}
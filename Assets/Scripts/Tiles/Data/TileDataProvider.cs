using Common;
using UnityEngine;

namespace Tiles.Data
{
    public class TileDataProvider : MonoObjectProvider<TileData>
    {
        [SerializeField] private TileData tileData;

        public override TileData Provide()
        {
            return tileData;
        }
    }
}
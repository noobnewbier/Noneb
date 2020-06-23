using Common;
using UnityEngine;

namespace Tiles
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
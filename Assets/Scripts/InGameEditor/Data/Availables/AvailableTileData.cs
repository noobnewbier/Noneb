using Tiles.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableTileData), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableTileData))]
    public class AvailableTileData : AvailableSet<TileData>
    {
    }
}
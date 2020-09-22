using Tiles.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableTileDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableTileDatas))]
    public class AvailableTileDatas : AvailableSet<TileDataScriptable>
    {
    }
}
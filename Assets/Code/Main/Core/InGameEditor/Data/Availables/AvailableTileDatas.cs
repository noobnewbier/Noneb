using Main.Core.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableTileDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableTileDatas))]
    public class AvailableTileDatas : AvailableSet<TileDataScriptable>
    {
    }
}
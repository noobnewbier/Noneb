using Main.Core.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableUnitDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableUnitDatas))]
    public class AvailableUnitDatas : AvailableSet<UnitDataScriptable>
    {
    }
}
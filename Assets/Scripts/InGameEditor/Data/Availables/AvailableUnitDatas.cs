using Units;
using Units.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableUnitDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableUnitDatas))]
    public class AvailableUnitDatas : AvailableSet<UnitDataScriptable>
    {
    }
}
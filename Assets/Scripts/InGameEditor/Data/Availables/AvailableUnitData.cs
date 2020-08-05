using Units;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableUnitData), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableUnitData))]
    public class AvailableUnitData : AvailableSet<UnitData>
    {
        
    }
}
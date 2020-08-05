using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableConstructData), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableConstructData))]
    public class AvailableConstructData : AvailableSet<ConstructData>
    {
        
    }
}
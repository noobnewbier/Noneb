using Main.Core.Game.Constructs.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableConstructDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableConstructDatas))]
    public class AvailableConstructDatas : AvailableSet<ConstructDataScriptable>
    {
    }
}
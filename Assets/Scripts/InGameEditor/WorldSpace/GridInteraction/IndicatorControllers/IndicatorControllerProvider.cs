using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    [CreateAssetMenu(fileName = nameof(IndicatorControllerProvider), menuName = MenuName.Providers + nameof(IndicatorController))]
    public class IndicatorControllerProvider : ScriptableGameObjectAndComponentProvider<IndicatorController>
    {
    }
}
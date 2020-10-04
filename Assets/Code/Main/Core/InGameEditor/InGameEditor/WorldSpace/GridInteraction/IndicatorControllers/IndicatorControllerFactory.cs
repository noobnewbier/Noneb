using Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    [CreateAssetMenu(fileName = nameof(IndicatorControllerFactory), menuName = MenuName.Providers + nameof(IndicatorController))]
    public class IndicatorControllerFactory : ScriptableGameObjectAndComponentFactory<IndicatorController>
    {
    }
}
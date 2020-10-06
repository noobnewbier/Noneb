using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    [CreateAssetMenu(fileName = nameof(IndicatorControllerFactory), menuName = MenuName.Providers + nameof(IndicatorController))]
    public class IndicatorControllerFactory : ScriptableGameObjectAndComponentFactory<IndicatorController>
    {
    }
}
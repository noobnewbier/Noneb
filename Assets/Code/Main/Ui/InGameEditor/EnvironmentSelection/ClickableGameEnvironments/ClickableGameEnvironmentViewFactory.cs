using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.EnvironmentSelection.ClickableGameEnvironments
{
    [CreateAssetMenu(fileName = nameof(ClickableGameEnvironmentViewFactory), menuName = MenuName.Factory + nameof(ClickableGameEnvironmentView))]
    public class ClickableGameEnvironmentViewFactory : ScriptableGameObjectAndComponentFactory<ClickableGameEnvironmentView>
    {
    }
}
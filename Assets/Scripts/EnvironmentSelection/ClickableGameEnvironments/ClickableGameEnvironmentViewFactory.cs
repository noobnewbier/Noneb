using Common.Factories;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace EnvironmentSelection.ClickableGameEnvironments
{
    [CreateAssetMenu(fileName = nameof(ClickableGameEnvironmentViewFactory), menuName = MenuName.Providers + nameof(ClickableGameEnvironmentView))]
    public class ClickableGameEnvironmentViewFactory : ScriptableGameObjectAndComponentFactory<ClickableGameEnvironmentView>
    {
    }
}
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace EnvironmentSelection.ClickableGameEnvironments
{
    [CreateAssetMenu(fileName = nameof(ClickableGameEnvironmentViewProvider), menuName = MenuName.Providers + nameof(ClickableGameEnvironmentView))]
    public class ClickableGameEnvironmentViewProvider : ScriptableGameObjectAndComponentProvider<ClickableGameEnvironmentView>
    {
    }
}
using Noneb.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.DataSelection.TabButton
{
    [CreateAssetMenu(fileName = nameof(TabButtonViewFactory), menuName = MenuName.Factory + nameof(TabButtonView))]
    public class TabButtonViewFactory : ScriptableGameObjectAndComponentFactory<TabButtonView>
    {
    }
}
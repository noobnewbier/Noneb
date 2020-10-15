using System;
using Noneb.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.DataSelection.TabButton
{
    [CreateAssetMenu(fileName = nameof(TabButtonViewModelFactory), menuName = MenuName.Factory + nameof(TabButtonViewModel))]
    public class TabButtonViewModelFactory : ScriptableObject, IFactory<Action, string, TabButtonViewModel>
    {
        public TabButtonViewModel Create(Action arg0, string arg1) => new TabButtonViewModel(arg0, arg1);
    }
}
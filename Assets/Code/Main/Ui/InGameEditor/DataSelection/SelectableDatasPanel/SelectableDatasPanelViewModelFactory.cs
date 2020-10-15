using Main.Core.Game.Common.Factories;
using Main.Core.InGameEditor.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.DataSelection.SelectableDatasPanel
{
    [CreateAssetMenu(
        fileName = nameof(SelectableDatasPanelViewModelFactory),
        menuName = MenuName.Factory + nameof(SelectableDatasPanelViewModelFactory)
    )]
    public class SelectableDatasPanelViewModelFactory : ScriptableObject, IFactory<EditorPalette, SelectableDatasPanelViewModel>
    {
        public SelectableDatasPanelViewModel Create(EditorPalette arg) => new SelectableDatasPanelViewModel(arg);
    }
}
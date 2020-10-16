using Noneb.Core.Game.Common.Factories;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectableDatasPanel
{
    [CreateAssetMenu(
        fileName = nameof(SelectableDatasPanelViewModelFactory),
        menuName = MenuName.Factory + nameof(SelectableDatasPanelViewModelFactory)
    )]
    public class SelectableDatasPanelViewModelFactory : ScriptableObject, IFactory<EditorPalette, SelectableDatasPanelViewModel>
    {
        [SerializeField] private SelectablePaletteDataViewModelFactory selectablePaletteDataViewModelFactory;

        public SelectableDatasPanelViewModel Create(EditorPalette arg) =>
            new SelectableDatasPanelViewModel(arg, selectablePaletteDataViewModelFactory);
    }
}
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.DataSelection.SelectableDatasPanel;
using Noneb.Ui.InGameEditor.DataSelection.TabButton;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.DataSelection.DataSelector
{
    [CreateAssetMenu(fileName = nameof(DataSelectorViewModelFactory), menuName = MenuName.Factory + nameof(DataSelectorViewModel))]
    public class DataSelectorViewModelFactory : ScriptableObject, IFactory<DataSelectorViewModel>
    {
        [SerializeField] private EditorPalette palette;
        [SerializeField] private TabButtonViewModelFactory tabButtonViewModelFactory;
        [SerializeField] private SelectableDatasPanelViewModelFactory panelViewModelFactory;


        public DataSelectorViewModel Create() => new DataSelectorViewModel(palette, tabButtonViewModelFactory, panelViewModelFactory);
    }
}
using Main.Core.Game.Common.Factories;
using Main.Core.InGameEditor.Data;
using Main.Ui.InGameEditor.DataSelection.SelectableDatasPanel;
using Main.Ui.InGameEditor.DataSelection.TabButton;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.DataSelection.DataSelector
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
using System;
using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Constructs;
using Main.Core.Game.Tiles;
using Main.Core.Game.Units;
using Main.Core.InGameEditor.Data;
using Main.Ui.InGameEditor.DataSelection.SelectableDatasPanel;
using Main.Ui.InGameEditor.DataSelection.TabButton;

namespace Main.Ui.InGameEditor.DataSelection.DataSelector
{
    public class DataSelectorViewModel
    {
        private readonly IFactory<Action, string, TabButtonViewModel> _tabButtonViewModelFactory;
        private readonly IFactory<SelectableDatasPanelViewModel> _panelViewModelFactory;
        private SelectableDatasPanelViewModel _selectableDatasPanelViewModel;

        public DataSelectorViewModel(EditorPalette palette,
                                     IFactory<Action, string, TabButtonViewModel> tabButtonViewModelFactory,
                                     IFactory<EditorPalette, SelectableDatasPanelViewModel> panelViewModelFactory)
        {
            TabButtonViewModelsLiveData = new LiveData<IReadOnlyList<TabButtonViewModel>>();
            SelectableDatasPanelViewModelLiveData = new LiveData<SelectableDatasPanelViewModel>();
            _panelViewModelFactory = Factory.Create(() => panelViewModelFactory.Create(palette));
            _tabButtonViewModelFactory = tabButtonViewModelFactory;
        }

        public ILiveData<IReadOnlyList<TabButtonViewModel>> TabButtonViewModelsLiveData { get; }
        public ILiveData<SelectableDatasPanelViewModel> SelectableDatasPanelViewModelLiveData { get; }

        private void ShowUnitPreset()
        {
            _selectableDatasPanelViewModel.ShowUnitPreset();
        }

        private void ShowConstructPreset()
        {
            _selectableDatasPanelViewModel.ShowConstructPreset();
        }

        private void ShowTilePreset()
        {
            _selectableDatasPanelViewModel.ShowTilePreset();
        }

        public void SetUpSubViews()
        {
            CreateTabs();
            CreatePanel();
            GotoDefaultTab();
        }

        private void CreateTabs()
        {
            var models = new List<TabButtonViewModel>
            {
                _tabButtonViewModelFactory.Create(ShowUnitPreset, nameof(Unit)),
                _tabButtonViewModelFactory.Create(ShowConstructPreset, nameof(Construct)),
                _tabButtonViewModelFactory.Create(ShowTilePreset, nameof(Tile))
            };

            TabButtonViewModelsLiveData.PostValue(models);
        }

        private void GotoDefaultTab()
        {
            ShowTilePreset();
        }

        private void CreatePanel()
        {
            _selectableDatasPanelViewModel = _panelViewModelFactory.Create();
            SelectableDatasPanelViewModelLiveData.PostValue(_selectableDatasPanelViewModel);
        }
    }
}
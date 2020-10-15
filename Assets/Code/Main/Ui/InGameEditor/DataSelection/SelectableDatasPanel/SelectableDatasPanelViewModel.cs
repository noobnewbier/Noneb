using System.Collections.Generic;
using System.Linq;
using Experiment.CrossPlatformLiveData;
using Main.Core.InGameEditor.Data;
using Main.Ui.InGameEditor.DataSelection.SelectablePaletteData;

namespace Main.Ui.InGameEditor.DataSelection.SelectableDatasPanel
{
    public class SelectableDatasPanelViewModel
    {
        private readonly IReadOnlyList<SelectablePaletteDataViewModel> _unitPresetsViewModels;
        private readonly IReadOnlyList<SelectablePaletteDataViewModel> _constructPresetsViewModels;
        private readonly IReadOnlyList<SelectablePaletteDataViewModel> _tilePresetsViewModels;

        public SelectableDatasPanelViewModel(EditorPalette palette)
        {
            SelectablePaletteDataViewModels = new LiveData<IReadOnlyList<SelectablePaletteDataViewModel>>();

            _unitPresetsViewModels = palette.AvailableUnitPresets.Datas
                .Select(d => new SelectablePaletteDataViewModel(d.Icon, d.Name))
                .ToList();

            _constructPresetsViewModels = palette.AvailableConstructPresets.Datas
                .Select(d => new SelectablePaletteDataViewModel(d.Icon, d.Name))
                .ToList();

            _tilePresetsViewModels = palette.AvailableTilePresets.Datas
                .Select(d => new SelectablePaletteDataViewModel(d.Icon, d.Name))
                .ToList();
        }

        public ILiveData<IReadOnlyList<SelectablePaletteDataViewModel>> SelectablePaletteDataViewModels { get; }

        public void ShowUnitPreset()
        {
            SelectablePaletteDataViewModels.PostValue(_unitPresetsViewModels);
        }

        public void ShowConstructPreset()
        {
            SelectablePaletteDataViewModels.PostValue(_constructPresetsViewModels);
        }

        public void ShowTilePreset()
        {
            SelectablePaletteDataViewModels.PostValue(_tilePresetsViewModels);
        }
    }
}
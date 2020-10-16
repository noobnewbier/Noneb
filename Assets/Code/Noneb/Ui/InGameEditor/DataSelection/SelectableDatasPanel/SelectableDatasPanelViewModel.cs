using System.Collections.Generic;
using System.Linq;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectableDatasPanel
{
    public class SelectableDatasPanelViewModel
    {
        private readonly IReadOnlyList<SelectablePaletteDataViewModel<PaletteData>> _unitPresetsViewModels;
        private readonly IReadOnlyList<SelectablePaletteDataViewModel<PaletteData>> _constructPresetsViewModels;
        private readonly IReadOnlyList<SelectablePaletteDataViewModel<PaletteData>> _tilePresetsViewModels;

        public SelectableDatasPanelViewModel(EditorPalette palette,
                                             IFactory<PaletteData, Sprite, string, SelectablePaletteDataViewModel<PaletteData>>
                                                 selectablePaletteDataViewModelFactory)
        {
            SelectablePaletteDataViewModels = new LiveData<IReadOnlyList<SelectablePaletteDataViewModel<PaletteData>>>();

            _unitPresetsViewModels = palette.AvailableUnitPresets.Datas
                .Select(d => selectablePaletteDataViewModelFactory.Create(d, d.Icon, d.Name))
                .ToList();

            _constructPresetsViewModels = palette.AvailableConstructPresets.Datas
                .Select(d => selectablePaletteDataViewModelFactory.Create(d, d.Icon, d.Name))
                .ToList();

            _tilePresetsViewModels = palette.AvailableTilePresets.Datas
                .Select(d => selectablePaletteDataViewModelFactory.Create(d, d.Icon, d.Name))
                .ToList();
        }

        public ILiveData<IReadOnlyList<SelectablePaletteDataViewModel<PaletteData>>> SelectablePaletteDataViewModels { get; }

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
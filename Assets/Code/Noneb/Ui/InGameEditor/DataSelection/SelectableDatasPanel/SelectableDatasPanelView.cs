using System.Collections.Generic;
using Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectableDatasPanel
{
    public class SelectableDatasPanelView : MonoBehaviour
    {
        [SerializeField] private Transform selectablePaletteDatasPanelTransform;
        [SerializeField] private SelectablePaletteDataViewFactory selectablePaletteDataViewFactory;
        private IList<SelectablePaletteDataView> _selectablePaletteDataViews;
        private SelectableDatasPanelViewModel _viewModel;

        public void Init(SelectableDatasPanelViewModel viewModel)
        {
            _viewModel = viewModel;
            _selectablePaletteDataViews = new List<SelectablePaletteDataView>();
            _viewModel.SelectablePaletteDataViewModels.Subscribe(OnSelectablePaletteDataViewModelsChange);
        }

        private void OnSelectablePaletteDataViewModelsChange(IReadOnlyList<SelectablePaletteDataViewModel> dataViewModels)
        {
            UpdateExistingPaletteDataViews(dataViewModels);
            CreateNewSelectableDataViewsIfNeeded(dataViewModels);
            CleanUpExcessiveSelectableDataViews(dataViewModels);
        }

        private void UpdateExistingPaletteDataViews(IReadOnlyList<SelectablePaletteDataViewModel> dataViewModels)
        {
            var maximumAmountOfUpdate = Mathf.Min(dataViewModels.Count, _selectablePaletteDataViews.Count);
            for (var i = 0; i < maximumAmountOfUpdate; i++)
                _selectablePaletteDataViews[i].Init(dataViewModels[i]);
        }

        private void CreateNewSelectableDataViewsIfNeeded(IReadOnlyList<SelectablePaletteDataViewModel> dataViewModels)
        {
            for (var i = _selectablePaletteDataViews.Count; i < dataViewModels.Count; i++)
            {
                var selectablePaletteDataView = selectablePaletteDataViewFactory.Create(
                        selectablePaletteDatasPanelTransform,
                        false
                    )
                    .component;

                _selectablePaletteDataViews.Add(
                    selectablePaletteDataView
                );

                selectablePaletteDataView.Init(dataViewModels[i]);
            }
        }

        private void CleanUpExcessiveSelectableDataViews(IReadOnlyCollection<SelectablePaletteDataViewModel> dataViewModels)
        {
            if (dataViewModels.Count < _selectablePaletteDataViews.Count)
            {
                for (var i = dataViewModels.Count; i < _selectablePaletteDataViews.Count; i++)
                {
                    _selectablePaletteDataViews[i].ReturnToPool();

                    _selectablePaletteDataViews.RemoveAt(i);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Main.Ui.InGameEditor.DataSelection.SelectableDatasPanel;
using Main.Ui.InGameEditor.DataSelection.TabButton;
using UniRx;
using UnityEngine;

namespace Main.Ui.InGameEditor.DataSelection.DataSelector
{
    public class DataSelectorView : MonoBehaviour
    {
        [SerializeField] private Transform tabsViewParent;
        [SerializeField] private TabButtonViewFactory tabButtonViewFactory;
        [SerializeField] private DataSelectorViewModelFactory viewModelFactory;
        [SerializeField] private SelectableDatasPanelView selectableDatasPanelView;

        private IDisposable _disposable;
        private DataSelectorViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.TabButtonViewModelsLiveData.Subscribe(OnCreateTabs),
                _viewModel.SelectableDatasPanelViewModelLiveData.Subscribe(OnCreatePanel)
            };
            _viewModel.SetUpSubViews();
        }

        private void OnCreateTabs(IReadOnlyCollection<TabButtonViewModel> buttonViewModels)
        {
            foreach (var buttonViewModel in buttonViewModels)
                tabButtonViewFactory
                    .Create(tabsViewParent, false)
                    .component
                    .Init(buttonViewModel);
        }

        private void OnCreatePanel(SelectableDatasPanelViewModel panelViewModel)
        {
            selectableDatasPanelView.Init(panelViewModel);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}
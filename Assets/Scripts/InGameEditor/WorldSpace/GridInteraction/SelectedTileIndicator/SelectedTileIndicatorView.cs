using System;
using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using JetBrains.Annotations;
using Tiles.Holders;
using UnityEngine;

namespace InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    public class SelectedTileIndicatorView : MonoBehaviour
    {
        [SerializeField] private SelectedTileIndicatorViewConfig config;
        [SerializeField] private SelectedTileIndicatorViewModelFactory viewModelFactory;

        private SelectedTileIndicatorViewModel _viewModel;
        private IndicatorController _selectedTileIndicatorController;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = _viewModel.CurrentlySelectedTileHolderLiveData.Subscribe(ShowIndicator);
            _selectedTileIndicatorController = config.IndicatorControllerFactory.Create().component;
        }

        private void ShowIndicator([CanBeNull] TileHolder tileHolder)
        {
            _selectedTileIndicatorController.ShowIndicator(tileHolder);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
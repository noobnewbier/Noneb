using System;
using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using JetBrains.Annotations;
using Tiles.Holders;
using UniRx;
using UnityEngine;

namespace InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    public class HoveredTileIndicatorView : MonoBehaviour
    {
        [SerializeField] private HoveredTileIndicatorViewConfig config;
        [SerializeField] private HoveredTileIndicatorViewModelFactory viewModelFactory;

        private HoveredTileIndicatorViewModel _viewModel;
        private IndicatorController _hoveredTileIndicatorController;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.CurrentlyHoveredTileHolderLiveData.Subscribe(OnHoveredTileUpdated),
                _viewModel.CurrentlySelectedTileHolderLiveData.Subscribe(OnSelectedTileUpdated)
            };
            _hoveredTileIndicatorController = config.IndicatorControllerFactory.Create().component;
        }

        private void OnHoveredTileUpdated([CanBeNull] TileHolder tileHolder)
        {
            if (_viewModel.CurrentlySelectedTileHolderLiveData.Value == tileHolder)
            {
                _hoveredTileIndicatorController.SetDisabled();
                return;
            }

            _hoveredTileIndicatorController.ShowIndicator(tileHolder);
        }

        private void OnSelectedTileUpdated([CanBeNull] TileHolder tileHolder)
        {
            if (_viewModel.CurrentlyHoveredTileHolderLiveData.Value == tileHolder)
            {
                _hoveredTileIndicatorController.SetDisabled();
            }
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
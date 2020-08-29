using System;
using JetBrains.Annotations;
using Tiles.Holders;
using UniRx;
using UnityEngine;

namespace InGameEditor.WorldSpace.GridInteraction
{
    public class TileSelectionView : MonoBehaviour
    {
        [SerializeField] private TileSelectionViewConfig config;
        [SerializeField] private TileSelectionViewModelFactory viewModelFactory;
        [SerializeField] private Transform mapTransform;


        private TileSelectionViewModel _viewModel;
        private IDisposable _disposable;
        private GameObject _selectedTileIndicator;
        private GameObject _hoveredTileIndicator;


        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create(mapTransform);

            _selectedTileIndicator = config.SelectedTileIndicatorProvider.Provide();
            _hoveredTileIndicator = config.HoveredTileIndicatorProvider.Provide();
            _selectedTileIndicator.SetActive(false);
            _hoveredTileIndicator.SetActive(false);

            _disposable = new CompositeDisposable
            {
                _viewModel.CurrentlySelectedTileHolderLiveData.Subscribe(OnUpdateSelectedTile),
                _viewModel.CurrentlyHoveredTileHolderLiveData.Subscribe(OnUpdateHoveredTile)
            };
        }

        //todo: handle cases where selected is same with hover
        //todo: handle show text on inspector on hover
        //todo: handle show info on inspector on select
        private void OnUpdateSelectedTile([CanBeNull] TileHolder tileHolder)
        {
            if (tileHolder == null)
            {
                _selectedTileIndicator.SetActive(false);
                return;
            }

            _selectedTileIndicator.transform.position = tileHolder.transform.position + Vector3.up * config.IndicatorOffsetFromMap;
        }

        private void OnUpdateHoveredTile([CanBeNull] TileHolder tileHolder)
        {
            if (tileHolder == null)
            {
                _hoveredTileIndicator.SetActive(false);
                return;
            }

            _hoveredTileIndicator.transform.position = tileHolder.transform.position + Vector3.up * config.IndicatorOffsetFromMap;
        }

        private void Update()
        {
            HandleMouseHover();
            HandleMouseClick();
        }

        private void HandleMouseHover()
        {
            _viewModel.OnHover(Input.mousePosition);
            _viewModel.OnHover(Input.mousePosition);
        }

        private void HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _viewModel.OnClicked(Input.mousePosition);
            }
        }

        private void OnDisable()
        {
            _disposable?.Dispose();

            Destroy(_hoveredTileIndicator);
            Destroy(_selectedTileIndicator);
        }
    }
}
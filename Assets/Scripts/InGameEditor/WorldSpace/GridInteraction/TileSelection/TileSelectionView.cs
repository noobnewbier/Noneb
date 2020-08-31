using UnityEngine;

namespace InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    //todo: handle show info on inspector on select
    public class TileSelectionView : MonoBehaviour
    {
        [SerializeField] private TileSelectionViewModelFactory viewModelFactory;
        [SerializeField] private Transform mapTransform;

        private TileSelectionViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create(mapTransform);
        }

        private void Update()
        {
            HandleMouseHover();
            HandleMouseClick();
        }

        private void HandleMouseHover()
        {
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
            _viewModel.Dispose();
        }
    }
}
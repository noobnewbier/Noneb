using UnityEngine;

namespace InGameEditor.WorldSpace.Maps
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private MapViewModelProvider viewModelProvider;
        private MapViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel = viewModelProvider.Provide();
        }
    }
}
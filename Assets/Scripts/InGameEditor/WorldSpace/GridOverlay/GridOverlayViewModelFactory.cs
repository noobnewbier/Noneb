using Maps.Repositories.CurrentMapConfig;
using Maps.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridOverlay
{
    [CreateAssetMenu(fileName = nameof(GridOverlayViewModelFactory), menuName = MenuName.Factory + nameof(GridOverlayViewModel))]
    public class GridOverlayViewModelFactory : ScriptableObject
    {
        [SerializeField] private GetCoordinateServiceProvider getCoordinateServiceProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;


        public GridOverlayViewModel Create(Transform centerTransform)
        {
            return new GridOverlayViewModel(
                getCoordinateServiceProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide(),
                tilesPositionServiceProvider.Provide(),
                centerTransform
            );
        }
    }
}
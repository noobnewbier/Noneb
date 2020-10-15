using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Core.Game.GameState.CurrentWorldConfig;
using Noneb.Ui.Game.Maps.TilesPosition;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridOverlay
{
    [CreateAssetMenu(fileName = nameof(GridOverlayViewModelFactory), menuName = MenuName.Factory + nameof(GridOverlayViewModel))]
    public class GridOverlayViewModelFactory : ScriptableObject
    {
        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;


        public GridOverlayViewModel Create(Transform centerTransform) =>
            new GridOverlayViewModel(
                coordinateServiceProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide(),
                tilesPositionServiceProvider.Provide(),
                centerTransform
            );
    }
}
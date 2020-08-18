using Maps;
using UnityEngine;
using UnityUtils.Constants;
using WorldConfigurations;

namespace InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    [CreateAssetMenu(fileName = nameof(CellOverlayViewModelFactory), menuName = MenuName.Factory + nameof(CellOverlayViewModel))]
    public class CellOverlayViewModelFactory : ScriptableObject
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public CellOverlayViewModel Create(Vector3 position,
                                           WorldConfig worldConfig,
                                           bool coordinateVisibility,
                                           bool lineVisibility,
                                           Coordinate coordinate)
        {
            return new CellOverlayViewModel(
                position,
                worldConfig,
                coordinateVisibility,
                lineVisibility,
                coordinate
            );
        }
    }
}
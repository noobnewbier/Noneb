using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    [CreateAssetMenu(fileName = nameof(CellOverlayViewProvider), menuName = MenuName.Providers + nameof(CellOverlayView))]
    public class CellOverlayViewProvider : PooledMonoBehaviourProvider<CellOverlayView>
    {
    }
}
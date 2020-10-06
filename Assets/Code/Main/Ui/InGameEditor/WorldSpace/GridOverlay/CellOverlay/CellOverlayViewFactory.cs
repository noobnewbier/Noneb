using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    [CreateAssetMenu(fileName = nameof(CellOverlayViewFactory), menuName = MenuName.Providers + nameof(CellOverlayView))]
    public class CellOverlayViewFactory : PooledMonoBehaviourFactory<CellOverlayView>
    {
    }
}
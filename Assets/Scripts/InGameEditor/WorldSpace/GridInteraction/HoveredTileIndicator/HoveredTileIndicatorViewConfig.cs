using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    [CreateAssetMenu(fileName = nameof(HoveredTileIndicatorViewConfig), menuName = MenuName.Data + nameof(HoveredTileIndicatorViewConfig))]
    public class HoveredTileIndicatorViewConfig : ScriptableObject
    {
        [SerializeField] private IndicatorControllerProvider indicatorControllerProvider;
        public IndicatorControllerProvider IndicatorControllerProvider => indicatorControllerProvider;
    }
}
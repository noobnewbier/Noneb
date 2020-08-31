using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    [CreateAssetMenu(fileName = nameof(SelectedTileIndicatorViewConfig), menuName = MenuName.Data + nameof(SelectedTileIndicatorViewConfig))]
    public class SelectedTileIndicatorViewConfig : ScriptableObject
    {
        [SerializeField] private IndicatorControllerProvider indicatorControllerProvider;

        public IndicatorControllerProvider IndicatorControllerProvider => indicatorControllerProvider;
    }
}
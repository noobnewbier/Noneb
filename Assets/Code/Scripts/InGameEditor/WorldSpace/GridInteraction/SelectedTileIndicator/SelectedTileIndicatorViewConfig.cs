using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    [CreateAssetMenu(fileName = nameof(SelectedTileIndicatorViewConfig), menuName = MenuName.Data + nameof(SelectedTileIndicatorViewConfig))]
    public class SelectedTileIndicatorViewConfig : ScriptableObject
    {
        [FormerlySerializedAs("indicatorControllerProvider")] [SerializeField]
        private IndicatorControllerFactory indicatorControllerFactory;

        public IndicatorControllerFactory IndicatorControllerFactory => indicatorControllerFactory;
    }
}
using InGameEditor.WorldSpace.GridInteraction.IndicatorControllers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    [CreateAssetMenu(fileName = nameof(HoveredTileIndicatorViewConfig), menuName = MenuName.Data + nameof(HoveredTileIndicatorViewConfig))]
    public class HoveredTileIndicatorViewConfig : ScriptableObject
    {
        [FormerlySerializedAs("indicatorControllerProvider")] [SerializeField] private IndicatorControllerFactory indicatorControllerFactory;
        public IndicatorControllerFactory IndicatorControllerFactory => indicatorControllerFactory;
    }
}
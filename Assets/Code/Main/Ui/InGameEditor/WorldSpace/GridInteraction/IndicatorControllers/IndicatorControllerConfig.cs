using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    [CreateAssetMenu(fileName = nameof(IndicatorControllerConfig), menuName = MenuName.Data + nameof(IndicatorControllerConfig))]
    public class IndicatorControllerConfig : ScriptableObject
    {
        [SerializeField] private float indicatorOffsetFromMap;

        public float IndicatorOffsetFromMap => indicatorOffsetFromMap;
    }
}
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewConfig), menuName = MenuName.Data + nameof(TileSelectionViewConfig))]
    public class TileSelectionViewConfig : ScriptableObject
    {
        [SerializeField] private GameObjectProvider selectedTileIndicatorProvider;
        [SerializeField] private GameObjectProvider hoveredTileIndicatorProvider;
        [SerializeField] private float indicatorOffsetFromMap;

        public GameObjectProvider SelectedTileIndicatorProvider => selectedTileIndicatorProvider;

        public GameObjectProvider HoveredTileIndicatorProvider => hoveredTileIndicatorProvider;

        public float IndicatorOffsetFromMap => indicatorOffsetFromMap;
    }
}
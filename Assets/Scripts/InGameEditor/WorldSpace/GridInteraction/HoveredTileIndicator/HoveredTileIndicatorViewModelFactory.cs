using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    [CreateAssetMenu(fileName = nameof(HoveredTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(HoveredTileIndicatorViewModel))]
    public class HoveredTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorCurrentHoveredTileHolderRepositoryProvider hoveredTileHolderRepositoryProvider;
        [SerializeField] private InGameEditorCurrentSelectedTileHolderRepositoryProvider selectedTileHolderRepositoryProvider;


        public HoveredTileIndicatorViewModel Create()
        {
            return new HoveredTileIndicatorViewModel(
                hoveredTileHolderRepositoryProvider.Provide(),
                selectedTileHolderRepositoryProvider.Provide()
            );
        }
    }
}
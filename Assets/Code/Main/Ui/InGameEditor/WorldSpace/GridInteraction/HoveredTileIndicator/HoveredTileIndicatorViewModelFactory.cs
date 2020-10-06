using Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder;
using Main.Ui.Game.Common.UiState.CurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    [CreateAssetMenu(fileName = nameof(HoveredTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(HoveredTileIndicatorViewModel))]
    public class HoveredTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider hoveredTileHolderRepositoryProvider;
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider selectedTileHolderRepositoryProvider;


        public HoveredTileIndicatorViewModel Create()
        {
            return new HoveredTileIndicatorViewModel(
                hoveredTileHolderRepositoryProvider.Provide(),
                selectedTileHolderRepositoryProvider.Provide()
            );
        }
    }
}
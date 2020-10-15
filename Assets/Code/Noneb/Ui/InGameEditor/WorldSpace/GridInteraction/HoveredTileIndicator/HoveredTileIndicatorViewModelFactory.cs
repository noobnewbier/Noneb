using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.HoveredTileIndicator
{
    [CreateAssetMenu(fileName = nameof(HoveredTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(HoveredTileIndicatorViewModel))]
    public class HoveredTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider hoveredTileHolderRepositoryProvider;
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider selectedTileHolderRepositoryProvider;


        public HoveredTileIndicatorViewModel Create() =>
            new HoveredTileIndicatorViewModel(
                hoveredTileHolderRepositoryProvider.Provide(),
                selectedTileHolderRepositoryProvider.Provide()
            );
    }
}
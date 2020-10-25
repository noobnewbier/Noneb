using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Ui.Game.UiState.ClickHandlingService;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using Noneb.Ui.InGameEditor.UiState;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject, IFactory<TileSelectionViewModel>
    {
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private ClosestTileHolderFromPositionServiceProvider closestTileHolderFromPositionServiceProvider;
        [SerializeField] private MousePositionServiceProvider mousePositionServiceProvider;
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [SerializeField] private SelectedMapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        public TileSelectionViewModel Create() =>
            new TileSelectionViewModel(
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                currentInspectableRepositoryProvider.Provide(),
                closestTileHolderFromPositionServiceProvider.Provide(),
                mousePositionServiceProvider.Provide(),
                selectedMapConfigRepositoryProvider.Provide()
            );
    }
}
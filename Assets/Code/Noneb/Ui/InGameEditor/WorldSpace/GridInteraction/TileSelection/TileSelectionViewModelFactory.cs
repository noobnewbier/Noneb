using Noneb.Core.Game.GameState.CurrentWorldConfig;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using Noneb.Ui.InGameEditor.Cameras;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;
        [SerializeField] private TileHoldersFetchingServiceProvider tileHolderRepositoryProvider;
        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadTilesHolderServiceProvider;
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        
        public TileSelectionViewModel Create(Transform mapTransform) =>
            new TileSelectionViewModel(
                currentWorldConfigRepositoryProvider.Provide(),
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                cameraRepositoryProvider.Provide(),
                mapTransform,
                tileHolderRepositoryProvider.Provide(),
                loadTilesHolderServiceProvider.Provide(),
                currentInspectableRepositoryProvider.Provide()
            );
    }
}